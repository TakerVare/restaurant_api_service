## NOTAS ##

## Controllers ##
- En esta carpeta se encuentran los controladores de la API RESTful del servicio de restaurante.
- Cada controlador maneja las solicitudes HTTP relacionadas con una entidad específica, como Menú Diario, Plato, Reserva, etc.
- Los controladores interactúan con los servicios para procesar las solicitudes y devolver las respuestas adecuadas.
- Los controladores están decorados con atributos que definen las rutas y los métodos HTTP que manejan (GET, POST, PUT, DELETE).
- Se utilizan modelos de datos y DTOs (Data Transfer Objects) para transferir información entre el cliente y el servidor.
- Los controladores implementan la lógica necesaria para validar las solicitudes, manejar errores y formatear las respuestas.

## BebidaController.cs ##
Este controlador maneja las operaciones relacionadas con las bebidas del restaurante. Proporciona endpoints para obtener todas las bebidas, obtener una bebida por su ID, crear una nueva bebida, actualizar una bebida existente y eliminar una bebida. Utiliza el servicio BebidaService para interactuar con la capa de negocio y acceder a los datos necesarios.

## ComboController.cs ##
Este controlador maneja las operaciones relacionadas con los combos del restaurante. Proporciona endpoints para obtener todos los combos, obtener un combo por su ID, crear un nuevo combo, actualizar un combo existente y eliminar un combo. Utiliza el servicio ComboService para interactuar con la capa de negocio y acceder a los datos necesarios.

## MenuDiarioController.cs ##
Este controlador maneja las operaciones relacionadas con los menús diarios del restaurante. Proporciona endpoints para obtener todos los menús diarios, obtener un menú diario por su ID y obtener el menú diario de una fecha específica. Utiliza el servicio MenuDiarioService para interactuar con la capa de negocio y acceder a los datos necesarios.

## PlatoPrincipalController.cs ##
Este controlador maneja las operaciones relacionadas con los platos principales del restaurante. Proporciona endpoints para obtener todos los platos principales, obtener un plato principal por su ID, crear un nuevo plato principal, actualizar un plato principal existente y eliminar un plato principal. Utiliza el servicio PlatoPrincipalService para interactuar con la capa de negocio y acceder a los datos necesarios.

## PostreController.cs ##
Este controlador maneja las operaciones relacionadas con los postres del restaurante. Proporciona endpoints para obtener todos los postres, obtener un postre por su ID, crear un nuevo postre, actualizar un postre existente y eliminar un postre. Utiliza el servicio PostreService para interactuar con la capa de negocio y acceder a los datos necesarios.