var getBackBtnHTML = '<li id="get_back_btn"><i class="glyphicon glyphicon-chevron-left"></i> Cofnij</li>';
var selectedCategoryIconHTML = '<i style="color: #FE980F" class="glyphicon glyphicon-ok"></i>'
var loadingAnimationHTML = '<div class="loading_animation"><div></div><div></div><div></div></div>'

var parentCategoryId;
var parentCategoryIdStack = [];

if ($("#Category").val().trim()) {
    $("#category_list").html(loadingAnimationHTML);

    $.get("/category/getcategory?id=" + $("#Category").val(), function (selectedCategory) {
        const categoryListHTML = convertToHtmlList(selectedCategory);
        $("#category_list").html(getBackBtnHTML);

        $("#category_list").append(categoryListHTML);
        $(".category_list_item").append(selectedCategoryIconHTML);
        $(".category_list_item").attr("class", "selected_category");  

        var reverseParentCategoryIdStack = [];

        var parentCategoryId = selectedCategory.parentId;
        reverseParentCategoryIdStack.push(parentCategoryId);
        
        while (parentCategoryId != 0) {            
            $.ajax({
                type: "GET",
                url: "/category/getcategory?id=" + parentCategoryId,
                async: false,
                success: function (parentCategory) {
                    parentCategoryId = parentCategory.parentId;
                }
            });

            reverseParentCategoryIdStack.push(parentCategoryId);
        }      

        while (reverseParentCategoryIdStack.length > 0) {
            parentCategoryIdStack.push(reverseParentCategoryIdStack.pop());
        }
    });

    $("#show_categories_btn").css("display", "none");

    $("#category_browser").css("display", "block");      
}

$("#show_categories_btn").on("click", function () {  
    $("#category_list").html(loadingAnimationHTML);

    $.get("/category/getrootcategories", function (rootCategories) {
        const categoryListHTML = convertToHtmlList(rootCategories);
        $("#category_list").html(categoryListHTML);
    });

    $(this).css("display", "none");

    $("#category_browser").css("display", "block");
});


$("#category_list").on("click", ".category_list_item", function () {
    var categoryId = $(this).attr("id").substring(3);
    parentCategoryIdStack.push($(this).attr("class").substring(29));
    console.log(parentCategoryIdStack);

    $("#category_list").html(loadingAnimationHTML);
   
    if ($(this).find("i").length) {
        $.get("/category/getchildrenof?id=" + categoryId, function (childCategories) {
            const categoryListHTML = convertToHtmlList(childCategories);

            $("#category_list").html(getBackBtnHTML);
            $("#category_list").append(categoryListHTML);
        });

    } else {
        $(this).attr("class", "selected_category");

        $(this).append(selectedCategoryIconHTML);

        $("#category_list").html(getBackBtnHTML);
        $("#category_list").append(this);

        $("#Category").val(categoryId);
    }    
});


$("#category_list").on("click", "#get_back_btn", function () {
    var parentCategoryId = parentCategoryIdStack.pop();

    $("#category_list").html(loadingAnimationHTML);

    if (parentCategoryId == 0) {     
        $.get("/category/getrootcategories", function (rootCategories) {
            const categoryListHTML = convertToHtmlList(rootCategories);
            $("#category_list").html(categoryListHTML);
        });

        $(this).css("display", "none");

    } else if (parentCategoryId > 0) {
        $.get("/category/getchildrenof?id=" + parentCategoryId, function (childCategories) {
            const categoryListHTML = convertToHtmlList(childCategories);

            $("#category_list").html(getBackBtnHTML);
            $("#category_list").append(categoryListHTML);
        });
    }
});


function convertToHtmlList(categories) {
    var listHTML = "";
    var categoryList

    if (!Array.isArray(categories)) {
        categoryList = [categories];
    } else {
        categoryList = categories;
    }

    categoryList.forEach(category => {
        listHTML += '<li id="cat' + category.id + '" class="category_list_item parent_cat' + category.parentId + '">' +
            category.name + (category.hasChildren ? '<i class="glyphicon glyphicon-chevron-right"></i>' : '') +
            '</li>' + '\n'
    });

    return listHTML;
}

