# Atm-Rod-Api

Esta es una aplicación web API desarrollada en .NET 6 que proporciona solucion para el challenge enviado por Metafar. En la solución se usan EF Code First, por lo tanto hay migraciones para crear la Base de datos e insertar datos en la misma.

# DER:
![DER](https://github.com/user-attachments/assets/3c8c20e1-bd01-49f6-a7f5-8692fe782b16)

# EndPoints:
![image](https://github.com/user-attachments/assets/7fc4a8ec-e50a-4c8b-aea8-15c4168bd27f)

- __Login__: Dado un numero de tarjeta y Pin, este devolvera un JWT, el cual es necesario para identifica ese usuario con esa tarjeta.

__Requiere un JWT para consumir los siguientes endpoints__

- __ConsultaPaginada__: Dado un numero de tarjeta, este devolvera un historial de las transacciones de ese numero de tarjeta previamente logeado, paginado por default en 10 registros.
- __Balance__:  Dado un numero de tarjeta, este devolvera el saldo de ese numero de tarjeta.
- __NuevaOperacion__:  Dado un numero de tarjeta y un monto, creara una transaccion para el numero de tarjeta informado.
