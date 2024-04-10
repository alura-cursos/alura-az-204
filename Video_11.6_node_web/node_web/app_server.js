let http = require('http');
let fs = require('fs');
let url = require('url');

let server = http.createServer(function(request, response) {

    let url_params = url.parse(request.url);
    let caminho = url_params.pathname;

    if(caminho == '/'){
        caminho = '/index.html';
    }

    fs.readFile(__dirname + caminho, function(erro, conteudo){
        if(erro){
            response.writeHead(404, { 'Content-Type': 'text/html'});
            response.write('<h1>Pagina não encontrada</h1>');
        } else {
            response.writeHead(200, { 'Content-Type': 'text/html'});
            response.write(conteudo);
        }
        response.end();
    });
});

server.listen(3000, function(){
    console.log('Servidor em execução');
});