#include "fingerprint_action.h"
#include "fingerprint_device.h"
#include "fingerprint_protocol.h"
#include "fingerprint_type.h"
#include <stdio.h>
#include <string.h>
#include <windows.h>

static U8Bit s_recv_buffer[256];
static U8Bit s_send_buffer[256];
static char resBuf[1024];

#ifndef _NDEBUG
#define _JDEBUG
static char dbg[1024];
void Debug(char *str) {
  OutputDebugStringA(str);
  MessageBoxA(NULL, str, "DebugWin", MB_OK);
}
#endif
void Delay(U32Bit msDelaytime) { Sleep(msDelaytime); }
#define TryTimes 10;

#define DLL_EXPORT __declspec(dllexport)
DLL_EXPORT float version(char *strVer) {
  sprintf(strVer, "Version: %d", 10001);
  // _JDEBUG sprintf(dbg, "sizeof(EM_LED_CTRL)=%ld sizeof(EM_LED_COLOR)=%ld",sizeof(EM_LED_CTRL),sizeof(EM_LED_COLOR)); Debug(dbg);
  return 1.0001;
}

DLL_EXPORT int FPDeviceOpen(const char *portname, int baudrate, char parity,
                            char databit, char stopbit, int timeout) {
  FP_action_set_send_buffer(s_send_buffer);
  FP_action_set_recv_buffer(s_recv_buffer);
  return FP_device_open(portname, baudrate, parity, databit, stopbit, timeout);
}

DLL_EXPORT S32Bit FPDeviceClose(void) {
  return FP_device_close();
}

DLL_EXPORT int setLed(EM_LED_CTRL crtl, EM_LED_COLOR color) {
  U32Bit errorCode = -1, tt = TryTimes;
  S32Bit ret = -1;
  FP_LED_CTRL_INFO stLEDCtrlInfo;

  stLEDCtrlInfo.ucLEDCtrlMode = crtl;
  stLEDCtrlInfo.ucLEDColor = color;
  stLEDCtrlInfo.usLEDpara1 = crtl==3? 0x64:0x16; //点亮时长
  stLEDCtrlInfo.usLEDpara2 = crtl==3? 0x00:0x10; //熄灭时长
  stLEDCtrlInfo.usLEDpara3 = crtl==3? 0x64:0xff; //闪烁次数
  while ((FP_OK != ret || 0 != errorCode) && tt-- > 0) {
    ret = FP_action_set_led(&stLEDCtrlInfo, &errorCode);
    Delay(50);
  };
  return errorCode;
}

DLL_EXPORT int getDeviceModuleID(char *strID) {
  FP_moduleID_t moduleID;
  U32Bit errorCode = 0;

  // U8Bit cmd[] = {0xF1, 0x1F, 0xE2, 0x2E, 0xB6, 0x6B, 0xA8, 0x8A, 0x00,
  //               0x07, 0x86, 0x00, 0x00, 0x00, 0x00, 0x03, 0x01, 0xFs_recv_bufferC};
  // U8Bit req[100];
  // FP_device_write_data(cmd, sizeof(cmd), 500);
  // Delay(100);
  // FP_device_read_data(req, 38, 500);
  // strcpy_s(strID, 16, req + 21);

  S32Bit ret = FP_action_getID(&moduleID, &errorCode);
  if (FP_OK == ret && 0 == errorCode) {
    strncpy_s(strID, 256, (char *)moduleID.ID, moduleID.length);
  }
  return errorCode;
}

DLL_EXPORT int enroll(void) {
  U32Bit errorCode;
  S32Bit ret;
  U8Bit index = 1;
  U8Bit isTouch;
  U16Bit SaveID;

  FP_enrollResult_t enrollResult;
  enrollResult.fingerID = 0;
  enrollResult.progress = 0;

  //第一次直接enrollStart
  goto EnrollStartLabel;

  //
  //每次enroll过后，确保手指要抬起来以后，再次按压
FingerLeaveLabel:
  FP_action_check_finger_is_touch(&isTouch, &errorCode);
  if (0 != isTouch || COMP_CODE_OK != errorCode) {
    // 当isTouch不是0的时候，意味着手指在上，
    //提示用户手指离开sensor再次按压
    // printf("lift your finger please !");
    //延时 200 ms
    Delay(200);
    goto FingerLeaveLabel;
  }

EnrollStartLabel:
  ret = FP_action_enroll_start(index, &errorCode);
  if (COMP_CODE_OK != errorCode || FP_OK != ret) {
    Delay(100);
    //可能上电初始化未完成或睡眠唤醒初始化中
    goto EnrollStartLabel;
  } else {
    //可延时100ms后发送获取注册结果命令，采图后需要大概这么长时间处理
    Delay(100);
  }

  //获取注册结果
EnrollResultLabel:
  setLed(EM_LED_CTRL_PWM, EM_LED_GREEN);
  ret = FP_action_get_enroll_result(&enrollResult, &errorCode);
  if (FP_DEVICE_TIMEOUT_ERROR == ret) {
    //接受数据超时，根据接受数据所设超时时间适当延时。
    Delay(100);
    goto EnrollResultLabel;
  }

  if (COMP_CODE_OK == errorCode) {
    //如果errorCode 是COMP_CODE_OK,说明本次enroll执行完成，
    //此时可以查看enrollResult.progress是否增加，如果增加，说明本次enroll成功

    //如果progress >= 100
    //，说明整个注册流程成功结束，开始根据enrollResult.fingerID保存指纹
    if (enrollResult.progress >= 0x64) {
      goto SaveStartLabel;
    } else {
      //如果progress < 100, 手指离开sensor，再次按压，继续注册
      index++;
      //延时 100 ms
      Delay(100);
      goto FingerLeaveLabel;
    }
  } else if (COMP_CODE_CMD_NOT_FINISHED == errorCode) {
    // errorCode ==
    // COMP_CODE_CMD_NOT_FINISHED意味着sensor还没有处理完指纹数据或没有按压手指，
    //适当延时，再次获取结果
    //延时 100 ms
    Delay(100);
    goto EnrollResultLabel;
  } else if (COMP_CODE_SAME_ID == errorCode) {
    // errorCode == COMP_CODE_SAME_ID意味着与已注册指纹重复，需换手指注册
    //适当延时，再次获取结果
    //延时 100 ms
    Delay(100);
    goto EnrollResultLabel;
  } else if (COMP_CODE_NO_FINGER_DETECT == errorCode) {
    // errorCode == COMP_CODE_NO_FINGER_DETECT意味着超时还没按压手指
    //重新注册
  } else if (COMP_CODE_OTHER_ERROR == errorCode) {
    goto EnrollStartLabel;
  } else if (COMP_CODE_NO_REQ_CMD == errorCode) {
    goto EnrollStartLabel;
  } else {
    //图像质量不好，手指可能按压过重、过轻、或者太过潮湿等，继续enrollStart即可，也可根据Elock用户手册细化处理
    //延时 200 ms
    Delay(200);
    goto EnrollStartLabel;
  }

  //保存指纹开始
  // enrollResult.fingerID会根据模组回复的推荐id去保存,编号从00开始
SaveStartLabel:
  ret = FP_action_save_start(enrollResult.fingerID, &errorCode);
  if (COMP_CODE_OK != errorCode || FP_OK != ret) {
    Delay(100);
    goto SaveStartLabel;
  }

  Delay(200);
  //获取保存指纹结果
SaveResultLabel
    :
    //【保存指纹开始命令】发送后，模组需操作flash，这期间大概200ms发送【获取保存指纹结果】没有数据回复，可超时重发
  ret = FP_action_get_save_result(&errorCode, &SaveID);
  if (FP_DEVICE_TIMEOUT_ERROR == ret) {
    //接受数据超时，根据接受数据所设超时时间适当延时。
    Delay(100);
    goto SaveResultLabel;
  }

  if (COMP_CODE_OK == errorCode) {
    //查看保存成功的SaveID
    //保存完成
  } else if (COMP_CODE_CMD_NOT_FINISHED == errorCode) {
    //还未保存完成，延时适当时间，再次获取结果
    //延时 100 ms
    Delay(100);
    goto SaveResultLabel;
  } else if (COMP_CODE_STORAGE_IS_FULL == errorCode) {
    // flash存储已满，不能保存指纹
  } else {
    //其他错误，比如硬件错误等。
  }
  setLed(0,0);
  return errorCode;
}

DLL_EXPORT int match(int* matchID, int* matchScore) {
  U32Bit errorCode;
  S32Bit ret;
  U8Bit isTouch;
  FP_matchResult_t matchResult;

/***********************
为了提速，也可以不先检查手指在位，
不同的模组回复时间不同(FPM08X系列模组大约耗时30ms左右)
************************/
checkFingeronLabel:
  FP_action_check_finger_is_touch(&isTouch, &errorCode);
  if ((0 != isTouch) && (COMP_CODE_OK == errorCode)) {
    //当isTouch是1的时候，意味着手指在上，
    //播放语音“滴”
  } else {
    //延时 50 ms
    Delay(50);
    goto checkFingeronLabel;
  }
/**********************/
matchStartLabel:
  //开始match
  ret = FP_action_match_start(&errorCode);
  if (COMP_CODE_OK != errorCode || FP_OK != ret) {
    //延时 50 ms
    //可能上电初始化未完成或睡眠唤醒初始化中
    Delay(50);
    goto matchStartLabel;
  }

  //匹配处理固定大于300ms;
  Delay(300);
  //获取注册结果
matchResultLabel:
  ret = FP_action_get_match_result(&matchResult, &errorCode);
  if (FP_DEVICE_TIMEOUT_ERROR == ret) {
    //接受数据超时，根据接受数据所设超时时间适当延时。
    Delay(100);
    goto matchResultLabel;
  }

  if (COMP_CODE_OK == errorCode) {
    if (1 == matchResult.isPass) {
      *matchID=matchResult.matchID;
      *matchScore=matchResult.matchScore;
    } else {
      *matchID=-1; *matchScore=0;
    }
  } else if (COMP_CODE_CMD_NOT_FINISHED == errorCode) {
    //还未匹配完成，延时适当时间，再次获取结果,处理指纹需要一些时间
    //延时 30 ms
    Delay(30);
    goto matchResultLabel;
  } else if (COMP_CODE_NO_FINGER_DETECT == errorCode) {
    // errorCode == COMP_CODE_NO_FINGER_DETECT意味着超时还没按压手指
    //重新匹配
  } else if (COMP_CODE_NO_REQ_CMD == errorCode) {
    goto matchStartLabel;
  } else if (COMP_CODE_HARDWARE_ERROR == errorCode) {
    //延时 200 ms
    Delay(200);
    goto matchStartLabel;
  } else {

  }
  return errorCode;
}

DLL_EXPORT int deleteFp(int delete_id) {
  U32Bit errorCode;
  S32Bit ret;

  //开始删除，delete_id > 0 的时候，删除指定id的指纹。
  // delete_id == -1的时候，删除所有指纹，
deleteStartLabel:
  ret = FP_action_delete_start(delete_id, &errorCode);
  if (COMP_CODE_OK != errorCode || FP_OK != ret) {
    //延时 100 ms
    //可能上电初始化未完成或睡眠唤醒初始化中
    Delay(100);
    goto deleteStartLabel;
  }

  //获取删除结果
deleteResultLabel:
  Delay(100); //延时 100 ms
  ret = FP_action_get_delete_result(&errorCode);
  if (ret == FP_DEVICE_TIMEOUT_ERROR) {
    goto deleteResultLabel;
  }

  if (COMP_CODE_OK == errorCode) {
    //删除成功
  } else if (COMP_CODE_CMD_NOT_FINISHED == errorCode) {
    //还未删除完成，再次获取结果
    goto deleteResultLabel;
  } else {
    //其他错误，比如指纹ID不存在等、flash硬件错误等。
  }

  return errorCode;
}
