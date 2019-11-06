# AET API VB Wrapper
This repo provides a visual basic wrapper for the AET API, documented at https://developers.aetrust.com/?version=latest.
The wrapper takes the inputs and constructs the signature and all other headers required by the AET API,
it sends the request to the API and outputs the response from the API without modifying it.

## Getting started
Import the 3 class files `AetrustHttpClient.vb`, `AetrustSignature.vb` & `AmericanEstateTrust.vb` to your project  

## Sample request using the wrapper classes
```
Dim apiKey = "123_my_key"
Dim apiSecret = "123_my_secret"
Dim aetApiUrl = "https://sandbox.aet.dev/v2/companies/users"
Dim myOrigin = "http://example.com"

Dim body = "{""data"":{""type"":""users"",""attributes"":{""email"":""johndoe+2@example.com"",""password"":""ystt^Yj3PL"",""confirmPassword"": ""ystt^Yj3PL""}}}"

Dim client = New AetrustHttpClient(New HttpClient(), apiSecret, apiKey, myOrigin)
Dim request = client.CreateRequest(aetApiUrl, New HttpMethod("POST"), body)
Dim bodyToSend = request.Content.ReadAsStringAsync().Result
Dim response = client.SendRequestAsync(request).Result
```
