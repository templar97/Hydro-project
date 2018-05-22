Imports Newtonsoft.Json
Imports System
Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks

Namespace HydroApi.Net
	Public Class HydroService
		Implements IHydroService

		Private Const BaseUrl As String = "https://api.hydrogenplatform.com/hydro/v1"

		Private httpClient As HttpClient

		Private Property BaseParameters() As Dictionary(Of String, String)

		Private privateApiKey As String
		Public Property ApiKey() As String Implements IHydroService.ApiKey
			Get
				Return privateApiKey
			End Get
			Private Set(ByVal value As String)
				privateApiKey = value
			End Set
		End Property

		Private privateApiUsername As String
		Public Property ApiUsername() As String Implements IHydroService.ApiUsername
			Get
				Return privateApiUsername
			End Get
			Private Set(ByVal value As String)
				privateApiUsername = value
			End Set
		End Property

		Public Sub New(ByVal apiKey As String, ByVal apiUsername As String)
			If String.IsNullOrEmpty(apiKey) OrElse String.IsNullOrEmpty(apiUsername) Then
				Throw New FormatException()
			End If

			Me.ApiKey = apiKey

			Me.ApiUsername = apiUsername

			httpClient = New HttpClient() With {.BaseAddress = New Uri(BaseUrl)}

			BaseParameters.Add("username", Me.ApiUsername)
			BaseParameters.Add("key", Me.ApiKey)
		End Sub

		Public Async Function RegisterAddress(ByVal address As String) As Task(Of String) Implements IHydroService.RegisterAddress
			Dim path As String = "/whitelist/{0}" & address

			Dim content As New StringContent(BaseParameters.ToString(), Encoding.UTF8)

			Dim response As HttpResponseMessage = Await httpClient.PostAsync(path, content).ConfigureAwait(False)

			Dim hydroAddressId As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

			Return hydroAddressId
		End Function

		Public Async Function RequestRaindrop(ByVal hydroAddressId As String) As Task(Of RaindropDetails) Implements IHydroService.RequestRaindrop
			Dim path As String = "/challenge?hydroAddressId=" & hydroAddressId

			Dim content As New StringContent(BaseParameters.ToString(), Encoding.UTF8)

			Dim response As HttpResponseMessage = Await httpClient.PostAsync(path, content).ConfigureAwait(False)

			Dim responseString As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

			Dim details As RaindropDetails = JsonConvert.DeserializeObject(Of RaindropDetails)(responseString)

			Return details
		End Function

		Public Async Function CheckValidRaindrop(ByVal hydroAddressId As String) As Task(Of Boolean) Implements IHydroService.CheckValidRaindrop
			Dim path As String = "/authenticate?hydroAddressId=" & hydroAddressId

			Dim content As New StringContent(BaseParameters.ToString(), Encoding.UTF8)

			Dim response As HttpResponseMessage = Await httpClient.PostAsync(path, content).ConfigureAwait(False)

			Dim responseString As String = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

			Return If(responseString = "true", True, False)
		End Function

	End Class
End Namespace
