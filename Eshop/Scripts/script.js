function hideFirstImg(element, photoUrl) {
    var x = element;
    x.style.backgroundImage = `url('${photoUrl}')`;
}
function showFirstImg(element, photoUrl) {
    var x = element;

    x.style.backgroundImage = `url('${photoUrl}')`;
}

function sImg(element, photo) {
    element.src = photo;
}

function fImg(element, photo) {
    element.src = photo;
}

function ChangeBorderColor(id) {
    $('.productsubimg1').css("border", "");
    $(`.productsubimg1:eq(${id})`).css({ "border-width": "1px", "border-color": "#ffc1c1", "border-style": "solid" });
}


$(document).ready(function () {
    $('.productsubimg1:first').css({ "border-width": "1px", "border-color": "#ffc1c1", "border-style": "solid" })
    jQuery('<div class="quantity-nav"><div class="quantity-button quantity-up">+</div><div class="quantity-button quantity-down">-</div></div>').insertAfter('.quantity input');
    jQuery('.quantity').each(function () {
        var spinner = jQuery(this),
            input = spinner.find('input[type="number"]'),
            btnUp = spinner.find('.quantity-up'),
            btnDown = spinner.find('.quantity-down'),
            min = input.attr('min'),
            max = input.attr('max');

        btnUp.click(function () {
            var oldValue = parseFloat(input.val());
            if (oldValue >= max) {
                var newVal = oldValue;
            } else {
                var newVal = oldValue + 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
        });

        btnDown.click(function () {
            var oldValue = parseFloat(input.val());
            if (oldValue <= min) {
                var newVal = oldValue;
            } else {
                var newVal = oldValue - 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
        });

    });
})