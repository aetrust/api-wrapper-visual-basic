# AET API VB Wrapper
This repo provides a visual basic wrapper for the AET API, documented at https://developers.aetrust.com/?version=latest

## Getting started
Import the 3 class files `AetrustHttpClient.vb`, `AetrustSignature.vb` & `AmericanEstateTrust.vb` to your project  

## Sample request using the wrapper classes
```
var apiKey = "123_my_key";
var apiSecret = "123_my_secret";
var aetApiUrl = "https://sandbox.aet.dev/v2/companies/users";
var myOrigin = "http://example.com";

var body = "{\"data\":{\"type\":\"users\",\"attributes\":{\"email\":\"johndoe+2@example.com\",\"password\":\"ystt^Yj3PL\",\"confirmPassword\": \"ystt^Yj3PL\"}}}";

var client = new AetrustHttpClient(new HttpClient(), apiKey, apiSecret, myOrigin);
var request = client.CreateRequest(aetApiUrl, new HttpMethod("POST"), body);
var bodyToSend = request.Content.ReadAsStringAsync().Result;
var response = client.SendRequestAsync(request).Result;
```
