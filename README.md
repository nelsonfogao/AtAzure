# AtAzure

Nesse Assessment, você deve criar com o Visual Studio, três projetos do tipo ASP.NET Web Application (.NET Framework).<br />

Duas dessas aplicações devem ser APIs web. Uma fornece operações de criação, leitura, atualização e deleção de amigos e também fornece operações de criação, leitura, 
atualização e deleção de relacionamento entre amigos. Outra fornece operações de criação, leitura, 
atualização e deleção de países e estados. Cada país deve possuir a foto da sua bandeira, um nome e sua lista de estados. 
Cada estado deve possuir a foto da sua bandeira e um nome. Cada amigo deve possuir uma foto, nome, sobrenome, e-mail, telefone, data de aniversário, 
sua lista de amigos, país de origem e estado de origem.<br />

A terceira aplicação deve ser do tipo MVC e deve conter as seguintes páginas:<br />

-cadastro de país<br />
-atualização de país<br />
-deleção de país<br />
-lista de países<br />
-detalhes de país<br />
-cadastro de estado<br />
-atualização de estado<br />
-deleção de estado<br />
-lista de estados<br />
-detalhes de estado<br />
-cadastro de amigo<br />
-atualização de amigo<br />
-deleção de amigo<br />
-lista de amigos<br />
-detalhes de amigo (lista de amigos daquele amigo)<br />
-cadastro de amigo de amigo (cadastra associação com amigo)<br />
-deleção de amigo de amigo (remove associação com amigo)<br />
Essa aplicação deve consumir as outras duas APIs Web através de requisições HTTP. Na página inicial dessa aplicação deve ser mantido uma contagem de países, estados e amigos.<br />

Essas aplicações devem ser implantadas no Microsoft Azure e os dados persistidos em um banco de dados SQL do Azure, onde devem ser criadas as tabelas para armazenamento dos dados dos amigos, dos relacionamentos entre amigos, do países de origem e dos estados de origem. 
Um amigo pode ter muitos amigos. Um amigo tem um país de origem e um estado de origem. Um país pode ter muitos estados.<br />

As operações de banco de dados devem ser implementadas como procedimentos armazenados e devem ser invocados a partir do código em C# na API web.
