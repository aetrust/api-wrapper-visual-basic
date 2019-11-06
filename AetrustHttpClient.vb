Imports System.Net.Http
Imports System.Text

Public Class AetrustHttpClient
    Private ReadOnly _client As HttpClient
    Private ReadOnly _secret As String
    Private ReadOnly _apiKey As String
    Private ReadOnly _origin As String

    Public Sub New(ByVal client As HttpClient, ByVal secret As String, ByVal apiKey As String, ByVal origin As String)
        _client = client
        _secret = secret
        _apiKey = apiKey
        _origin = origin
    End Sub

    Public Overridable Function CreateRequest(ByVal url As String, ByVal method As HttpMethod, ByVal Optional body As String = Nothing, ByVal Optional serverCurrent As DateTime? = Nothing) As HttpRequestMessage

        Dim message = New HttpRequestMessage With {
            .RequestUri = New System.Uri(url),
            .Method = method
        }

        Dim methodAsString = method.Method.ToUpper()
        Dim signature = AmericanEstateTrust.GenerateSignature(_secret, message.RequestUri.AbsolutePath, If(serverCurrent, DateTime.UtcNow), methodAsString, body)

        message.Headers.Add("Signature", signature.Signature)
        message.Headers.TryAddWithoutValidation("Content-Type", "application/vnd.api+json")
        message.Headers.Add("Timestamp", signature.Timestamp.ToString())
        message.Headers.Add("ApiKey", _apiKey)
        message.Headers.Add("Origin", _origin)

        If (body IsNot Nothing) Then
            message.Content = New StringContent(body, Encoding.UTF8, "application/vnd.api+json")
        End If

        Return message

    End Function

    Public Overridable Async Function SendRequestAsync(ByVal message As HttpRequestMessage) As Task(Of HttpResponseMessage)
        Return Await _client.SendAsync(message)
    End Function

End Class