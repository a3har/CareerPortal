function Delete(url) {
    swal({
        title: "Are you sure you want to delete ?",
        text: "This is a permanent action and you will not be able to undo it",
        buttons: true,
        dangerMode: true,
        icon: "warning"
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        location.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}