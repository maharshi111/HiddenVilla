
window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 10000 });
    }
}

// I need to add cdn of  css and JS and also jQUERY IN _hOST.csHtml, in order to use Toastr

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire({
            title: "Good job!",
            text: message,
            icon: "success"
        });
    }
    if (type === "error") {
        Swal.fire({
            title: "Error Notification!",
            text:  message,
            icon: "error"
        });
    } 

}