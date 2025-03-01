
# Mercado Pago - Backend Checkout API

El proyecto es una API que contiene varios de los endpoints de la [documentación de MercadoPago](https://www.mercadopago.com.ar/developers/es/reference) sin necesidad de usar el SDK.

## 🔦 Requerimientos
- [Visual Studio IDE](https://visualstudio.microsoft.com/es/)
- [.NET 8 SDK](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/es-ar/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/)

## 🏕️ Variables de entorno

Antes de ejecutar el proyecto hay que hacer una copia del archivo appsettings.Template.json, renombrarlo a appsettings.json y configurar los valores faltantes:

Conexion a la base de datos:

`ConnectionStrings:StoreDB`

Credenciales de aplicación a integrar ([Ver panel](https://www.mercadopago.com.ar/developers/panel/app)): 

`MercadoPago:AccessToken`

`MercadoPago:PublicKey`

Configuracion para generar el Json Web Token:

`Jwt:Issuer` (Para pruebas se puede usar la URL de localhost)

`Jwt:SecretKey` (Un GUID o cualquier cosa escrita con mas de 32 caracteres)

## 💻 Instalacion
1. Abrir la solución en Visual Studio
2. Ejecutar el comando `Update-Database` en la consola del Package Manager seleccionando como proyecto de inicio: `src\MercadoPago.CheckoutAPI.Infrastructure`
3. Abrir Postman 
4. Importar la [Coleccion y Entorno](MercadoPago/MercadoPago.CheckoutAPI/Postman)
5. Ajustar la URL del entorno para que coincida con la del archivo launchSettings.json

## 🔌 Uso

### Generación de Token JWT
1. Ejecutar `POST /Auth`

### Creando un pago:
1. Ejecutar `POST /CardTokens`
2. Ejecutar `GET /PaymentMethods/Search`
3. Ejecutar `GET /PaymentMethods/Installments` _(Opcional para ver las cuotas disponibles y modificar en `POST /Payments`)_
4. Ejecutar `POST /Payments`

### Creando un cliente y tarjeta:
1. Ejecutar `POST /Customers`
2. Ejecutar `POST /CustomerCards/{{CustomerID}}/Cards`

## 📄 Referencias
- [Mercado Pago - CheckoutAPI](https://www.mercadopago.com.ar/developers/es/docs/checkout-api/landing)
- [Mercado Pago - API Reference](https://www.mercadopago.com.ar/developers/es/reference)

