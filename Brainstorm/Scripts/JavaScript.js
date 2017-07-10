$(document).ready(function () {
    // collapse button change when hide panel1
    $('#collapse1').on('hide.bs.collapse',
        function() {
            //$('#min').attr('src', '/Images/Maximiza.gif');
            $('#min').removeClass('fa-window-minimize');
            $('#min').addClass('fa-window-maximize');
            $('#min').attr('title', 'Mostrar');

        });
    $('#collapse1').on('show.bs.collapse',
        function () {
            $('#min').removeClass('fa-window-maximize');
            $('#min').addClass('fa-window-minimize');
            $('#min').attr('title', 'Esconder');

        });
    // collapse button change when hide panel2
    $('#collapse2').on('hide.bs.collapse',
        function (e) {
            // if adicionado, uma vez que esta funcao era activada quando era feito o toggle de outros eventos
            if ($(this).is(e.target)) {
                $('#min2').removeClass('fa-window-minimize');
                $('#min2').addClass('fa-window-maximize');               
                $('#min2').attr('title', 'Mostrar');
            }       

        });
    $('#collapse2').on('show.bs.collapse',
        function () {
            $('#min2').removeClass('fa-window-maximize');
            $('#min2').addClass('fa-window-minimize');
            $('#min2').attr('title', 'Esconder');

        });

    // collapse button change when hide panel3
    $('#collapse3').on('hide.bs.collapse',
        function () {
            $('#min3').removeClass('fa-window-minimize');
            $('#min3').addClass('fa-window-maximize');
            $('#min3').attr('title', 'Mostrar');

        });
    $('#collapse3').on('show.bs.collapse',
        function () {
            $('#min3').removeClass('fa-window-maximize');
            $('#min3').addClass('fa-window-minimize');
            $('#min3').attr('title', 'Esconder');

        });

    $(function() {
        $('#Data').datetimepicker({        
            format: 'DD/MM/YYYY'
        });
    });


    // for each select option(interveniente) add the select2 plugin
    $('select').each(function(index) {
        var teste = ".selectInterveniente" + (index+1);       
        $(teste).select2();
    });
    // submit the form when click on button

    $('#gravar').on('click', function (event) {
        event.preventDefault();       
        $('#myForm').submit();
    });

    $('#gravar1').on('click', function (event) {
        event.preventDefault();
        $('.estado').val('A');
        $('.estadoFlag').val(true);
        $('#myForm').submit();
    });

    $('#gravar2').on('click', function (event) {
        event.preventDefault();
        $('.estado').val('E');
        $('.estadoFlag').val(true);
        $('#myForm').submit();
    });

    $('#gravar3').on('click', function (event) {
        event.preventDefault();
        $('.estado').val('X');
        $('.estadoFlag').val(true);
        $('#myForm').submit();
    });

    // mudar a imagem ou icon para botao de cada tema
    $('.mostraGestInov').each(function (index) {
        $('#Foo' + (index)).on('show.bs.collapse',
            function() {
                $('#imagem' + (index)).attr('src', '/Images/menos.gif');

            });
    });

    $('.mostraGestInov').each(function (index) {
        $('#Foo' + (index)).on('hide.bs.collapse',
            function () {
                $('#imagem' + (index)).attr('src', '/Images/mais.gif');

            });
    });



    /*MODAL */

    $("#wf5").click(function() {
        $("#myModal").modal('show');
    })

    $("#wf").click(function () {
        //alert("entrei");
        $("#modalWorkflow").modal('show');
    })

});

//console.log("entr");
