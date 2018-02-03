Push.create("HOLA INGENEO!", {
    body: "Hay un servicio proximo a terminar, enviamos toda la información relacionada via Email, o Clickea este enlace para verlo ahora.",
    icon: '/fonts/ingeneo.jpg',    
    timeout: 300000,
    onClick: function () {
        window.focus();
        window.open('/Notification/Index', '_self');
        //this.close();
    }    
});

