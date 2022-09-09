# Tubaina 🏺

Solução de emissão de Guias de Serviço autenticáveis.

## Tubaina API
API RESTful em .NET Core. Os documentos são embarcados no Swagger (`/swagger/index.html`).

A interface permite solicitar PDFs para impressão de guias de serviço. A guia pode ou não ser rastrável, por mais que todas as guias possuam uma assinatura digital.

## Tubaina Encoder
Livraria em .NET Core. Permite converter objetos de solicitação de serviço em PDFs formatados.
Guias de Serviço podem ser geradas a partir da classe `ServiceHandler`. O `ServiceHandler` recebe um objeto de `ServiceRequest` com um campo de informação genérica e um campo de URL de arquivo opcionais. O campo de indentificação do Operador é obrigatório.
