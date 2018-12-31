$(() => {
    var attrName = "data-button-action";

    $(`button[${attrName}]`).on("click", e => {
        var action = $(e.currentTarget).attr(attrName);



    });
});