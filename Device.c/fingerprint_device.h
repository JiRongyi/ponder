
#ifndef FINGERPRINT_DEVICE_H
#define FINGERPRINT_DEVICE_H

#include "fingerprint_type.h"

#ifdef __cplusplus
extern "C" {
#endif

S32Bit FP_device_open(const char *portname, int baudrate, char parity,  char databit, char stopbit, U32Bit timeout);
S32Bit FP_device_close();

/*************** You should implement the following functions ***************/
S32Bit FP_device_read_one_byte(U8Bit *data, U32Bit timeout);
S32Bit FP_device_read_data(U8Bit *data, U32Bit length, U32Bit timeout);
S32Bit FP_device_write_data(U8Bit *data, U32Bit length, U32Bit timeout);

#ifdef __cplusplus
}
#endif

#endif // FINGERPRINT_DEVICE_H
