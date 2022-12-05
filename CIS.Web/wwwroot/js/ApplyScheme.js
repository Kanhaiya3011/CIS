
$('#select-ben').on('change', function () {
    console.log("list item selected");
    var vals = $(this).val();
    var url = 'https://localhost:7098/User/SchemeApplied?val=' + vals;
    // debugger;
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        crossdomain: true,
        success: function (data) {
            $('#not-applied-schemes').html(data);

        },
        error: function (error) {

            console.log(error);
        }
    });
    console.log(vals);
});
