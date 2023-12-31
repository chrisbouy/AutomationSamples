      This JMeter project hits various authorization end points of a website called HTTPBin. 
      
      EXPLANATION OF USE CASES:
      
      Basic-HTTP Authorization Manager:
            uses basic authentication for 2 requests.  2nd request is just to show how to extract a variable, modify it,
            and use it in another request. Here, the variable is the username.  This assumes that both usernames have
            the same password, so it's not a real-world example.
      Basic-JSR223 PreProcessor:
            uses Java to encode the credentials.
      Basic - base64Encode function:
            uses a built-in JMeter function to encode the credentials.
      Bearer Token:
            Posts credentials to an end point that creates a bearer token.  Then pulls token from response to use in Header Manager
      Digest-HTTP Authorization Manager:
            uses built-in Digest mechanism in Authorization Manager.
      Digest-Custom:
            uses Java to build digest
            
      ...more test being created
      

   
