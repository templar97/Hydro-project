Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks

Namespace HydroApi.Net
	Public Interface IHydroService
		ReadOnly Property ApiKey() As String

		ReadOnly Property ApiUsername() As String

		Function RegisterAddress(ByVal address As String) As Task(Of String)

		Function RequestRaindrop(ByVal hydroAddressId As String) As Task(Of RaindropDetails)

		Function CheckValidRaindrop(ByVal hydroAddressId As String) As Task(Of Boolean)
	End Interface
End Namespace
