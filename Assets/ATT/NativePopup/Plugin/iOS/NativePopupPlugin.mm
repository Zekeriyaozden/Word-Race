#import <Foundation/Foundation.h>

@interface NativePopupPlugin : NSObject

@end

typedef void (*Callback)();

@implementation NativePopupPlugin

+(void)alertView:(NSString*)title addMessage:(NSString*) message addButton:(NSString*) button  addCallBack:(Callback) callback
{
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:title
                                message:message preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction *defaultAction = [UIAlertAction actionWithTitle:button style:UIAlertActionStyleDefault
                                        handler:^(UIAlertAction *action){
                                            NSLog(@"===== Alert action =====");
                                            callback();
                                        }];
    
    [alert addAction:defaultAction];
    [UnityGetGLViewController() presentViewController:alert animated:YES completion:nil];
}

char* cStringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    
    return res;
}

@end


extern "C"
{
    void _ShowAlert(const char *title, const char *message, const char *button, Callback callback)
    {
        [NativePopupPlugin
            alertView:[NSString stringWithUTF8String:title]
            addMessage:[NSString stringWithUTF8String:message]
            addButton:[NSString stringWithUTF8String:button]
            addCallBack:(Callback) callback];
    }
        
}

