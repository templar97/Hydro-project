# HydroApi.Net


A Wrapper for Hydro Blockchain Authentication API written in visual basic(vb)

 ## Features

 - Compitable with **.NET Standard 1.6**

## Getting Started

    Dim hydroService As New HydroService("API_KEY", "API_USERNAME")

### Step 1: Add address to whitelist

    Dim hydroAddressId As String = Await hydroService.RegisterAddress("ACCESSOR_ETH_PUBLIC_ADDRESS")

### Step 2: Get Raindrop details

    Dim raindropDetails As RaindropDetails = Await hydroService.RequestRaindrop("ACCESSOR_HYDRO_ADDRESS_ID")

### Step 3: Check on exist valid Raindrop transaction

    Dim hasValidTransaction As Boolean = Await hydroService.CheckValidRaindrop("ACCESSOR_HYDRO_ADDRESS_ID")

	If hasValidTransaction Then
		' Continue next step of authentication
	Else

	End If

 ## Information
  Check API [FAQ](https://github.com/hydrogen-dev/hydro-docs) for more details...
