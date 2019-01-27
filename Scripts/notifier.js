var alertTypes = {
    success : "success",
    error : "error",
    info : "info"
}

function notify(type, message) {
    let notifyModal = $("#alertModal");
    //notifyModal.find(".modal-title").html("");
    notifyModal.addClass(type);
    notifyModal.find(".glyphicon");
   
    notifyModal.find(".modal-body").html(message);
    notifyModal.modal("show");
}