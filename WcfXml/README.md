##WcfXml.exe
Utility to convert from WCF SOAP binary to XML and vice versa. Developed by Khai Tran (@k_tr4n).

###Usage:

     wcfxml.exe <CONVERT OPTIONS> -f <File location> [OUTPUT OPTIONS]

 Decode example: ```wcfxml.exe -d -f test.bin -s```

 Encode example: ```wcfxml.exe -e -f test.bin -b64```

###Options:
     -d, --decode               WCF binary to XML converter
       
     -e, --encode               XML to WCF converter
       
     -f, --file=VALUE           File location
       
           --b64, --out-base64    Encode output in Base64 format
           
     -s, --out-string           Encode output in String format (default)
       
     --dict=VALUE           Use external dictionary (using Microsoft
                                    dictionary by default)
                               
     --spec=VALUE           Specify NBFS specification (1 or 2). Default 2
           
     -h, --help                 show this message and exit
    

