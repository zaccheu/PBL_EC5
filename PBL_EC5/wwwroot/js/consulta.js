
document.getElementById('btnPesquisar').addEventListener('click', function () {
    e.preventDefault();
    var formData = $('#filtroUsuarios').serialize();
    $.ajax({
        url: '/Usuario/Pesquisar',
        type: 'POST',
        data: formData,
        success: function (html) {
            $('#usuarios-tbody').html(html);
        }
    });
});
