var imgCounter = 0;
var imgId = 0;
var mainImgId;

var imgHolderHTML = `<div class="img_holder">
                         <div class="img_preview">
                             <label class="fa fa-plus img_add">
                                 <input type="file" name="ImageFiles" multiple="multiple" accept="image/png, image/jpeg" class="upload_img">
                             </label>
                         </div>
                     </div>`

var imgDeleteHTML = '<div class="img_delete"><span>USUŃ</span></div>';
var mainImgHTML = '<div id="main_img">GŁÓWNE</div>';

var selectedImgBorderStyle = "3px solid #FE980F";
var notSelectedImgBrderStyle = "3px solid #FFEBD1";

var selected_img;

if ($("#img_panel").children().length > 0) {
    imgCounter = $(".img_holder").length - 1;
    imgId = $("#img_panel").last().before().find(".img_holder").attr("id").substring(3);
    mainImgId = $("#img_panel").first().find(".img_holder").attr("id").substring(3);

    selected_img = $("#img_panel").first().find(".img_preview");

    configureSelectedImg(selected_img);
}

$(document).on("change", ".upload_img", function () {
    var upload_img = $(this);
    var files = !!this.files ? this.files : [];

    if (!files.length || !window.FileReader) return;

    if (/^image/.test(files[0].type)) {
        imgCounter++;
        imgId++;

        var reader = new FileReader();
        reader.readAsDataURL(files[0]);

        reader.onloadend = function () {
            var imgURL = "url(" + this.result + ")";

            if (imgCounter == 1) {
                $("#images_preview").css("padding-top", "55.5%");
                $("#add_first_img").css("display", "none");
                $("#img_panel").append(imgHolderHTML);
                selected_img = $("#img_panel").find(".img_preview");
            }

            if (imgCounter > 1) {
                selected_img.css("border", notSelectedImgBrderStyle);
                selected_img = upload_img.closest(".img_preview");
                $("#set_main_img").css("display", "block");
            }

            selected_img.closest(".img_holder").attr("id", "img" + imgId);
            selected_img.css("background-image", imgURL).css("background-color", "#FFF").css("cursor", "pointer").css("border", selectedImgBorderStyle);
            selected_img.after(imgDeleteHTML);
            selected_img.find(".img_add").css("display", "none");

            if (imgCounter < 8) {
                selected_img.closest(".img_holder").after(imgHolderHTML);

                if (imgCounter == 1) {
                    $("#images_preview").css("display", "block");
                    mainImgId = imgId;
                    selected_img.append(mainImgHTML);
                }

                $("#images_preview").css("background-image", imgURL);
            }

            configureSelectedImg(selected_img);
        }
    }
});

$("#set_main_img").on("click", function () {
    $("#img" + mainImgId).find("#main_img").remove();
    mainImgId = selected_img.closest(".img_holder").attr("id").substring(3);

    selected_img.append(mainImgHTML);

    $(this).css("display", "none");
});

function configureSelectedImg(selected_img) {
    selected_img.on("click", function () {
        selected_img.css("border", notSelectedImgBrderStyle);

        $(this).css("border", selectedImgBorderStyle);

        var imgURL = $(this).css("background-image");

        if (imgURL != "none") {
            $("#images_preview").css("background-image", imgURL);

            if ($(this).closest(".img_holder").attr("id") != "img" + mainImgId) $("#set_main_img").css("display", "block");
            else $("#set_main_img").css("display", "none");

            selected_img = $(this);
        }
    });

    selected_img.closest(".img_holder").find(".img_delete").on("click", function () {
        if ($(this).closest(".img_holder").find(".img_preview").is(selected_img)) {
            if (imgCounter == 1) {
                $("#set_main_img").css("display", "none");
                $("#add_first_img").css("display", "block");
                $("#images_preview").css("background-image", "").css("padding-top", "40%");
                $(".img_holder").remove();
            }

            if (imgCounter == 2) {
                $("#set_main_img").css("display", "none");
            }

            if (imgCounter > 1) {
                var imgURL;

                if (mainImgId != $(this).closest(".img_holder").attr("id").substring(3)) {
                    selected_img = $("#img" + mainImgId).find(".img_preview");

                    imgURL = selected_img.css("background-image");
                    selected_img.css("border", selectedImgBorderStyle);

                    $("#images_preview").css("background-image", imgURL);
                }
            }
        }

        if (mainImgId == $(this).closest(".img_holder").attr("id").substring(3)) {
            for (i = 0; i <= imgId; i++) {
                if ($("#img" + i).length && i != mainImgId) {
                    console.log("fsdfsd");

                    mainImgId = i;
                    $("#img" + mainImgId).find(".img_preview").append(mainImgHTML);
                    i = imgId;
                }
            }

            if ($(this).closest(".img_holder").find(".img_preview").is(selected_img)) {
                selected_img = $("#img" + mainImgId).find(".img_preview");

                imgURL = selected_img.css("background-image");
                selected_img.css("border", selectedImgBorderStyle);

                $("#images_preview").css("background-image", imgURL);
            }
        }

        if (imgCounter == 8) {
            $("#img" + imgId).after(imgHolderHTML);
        }

        $(this).closest(".img_holder").remove();

        imgCounter--;
    });
}