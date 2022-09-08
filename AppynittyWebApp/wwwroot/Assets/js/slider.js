
////var url=[{"appId":0,"name":"Splash Screen","icon":"icon ai-splashscreen","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/splashscreen.png"},{"appId":1,"name":"Home","icon":"icon ai-demo-home","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/home.png"},{"appId":2,"name":"Menu","icon":"icon ai-demo-menu","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/menu.png"},{"appId":3,"name":"About Us","icon":"icon ai-demo-aboutus","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/aboutus.png"},{"appId":4,"name":"Gallery","icon":"icon ai-demo-gallery","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/gallery.png"},{"appId":5,"name":"Location","icon":"icon ai-demo-location","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/location.png"},{"appId":6,"name":"News and Media","icon":"icon ai-demo-events","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/newsandmedia.png"},{"appId":7,"name":"Product Listing","icon":"icon ai-demo-productlisting","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/floatinginappcall.png"},{"appId":8,"name":"Push Notification","icon":"icon ai-demo-pushnotification","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/pushnotification.png"},{"appId":9,"name":"Online Payment Gateway","icon":"icon ai-demo-onlinepayment","image":"https:\/\/www.appynitty.com\/uploads\/product\/realestateappy\/onlinepayment.png"}]
$(function () {    
function loadSlider(url,name){
    $.post(url, {product:name },function (data) {
        
        var app = JSON.parse(data);
        var count = app.length;
        var half = Math.round(count / 2);

        function init() {

            if ($(document).width() > 590) {
                for (i = 0; i < half; i++) {
                    $(".leftlist").append('<div class="list" id="app' + app[i].appId + '"><div class="c-name">' + app[i].name + '</div><div class="c-icon"><div class="' + app[i].icon + '"></div></div></div>');
                }
                for (i = half; i < count; i++) {
                    $(".rightlist").append('<div class="list" id="app' + app[i].appId + '"><div class="c-name">' + app[i].name + '</div><div class="c-icon"><div class="' + app[i].icon + '"></div></div></div>');
                }
            }
            else {
                for (i = 0; i < count; i++) {
                    $(".rightlistt").append('<div class="list" id="app' + app[i].appId + '"><div class="c-name">' + app[i].name + '</div><div class="c-icon"><div class="' + app[i].icon + '"></div></div></div>');
                }
                $('.rightlistt').slick({
                    dots: false,
                    arrows: true,
                    asNavFor: '.sliderExample',
                });
            }

        }
        ;
        init();


        $('.sliderExample').slick({
            dots: false,
            arrows: false,
            autoplay: false,
            autoplaySpeed: 2000,
        });
        for (i = 0; i < count; i++) {
            $('.sliderExample').slick('slickAdd', '<div><img src="' + app[i].image + '" class="img img-responsive"></div>');
        }
        $('.sliderExample').slick('slickGoTo', 0, false);
        $(".left-arrow").click(function () {
            $('.sliderExample').slick('slickPrev');
        });
        $(".right-arrow").click(function () {
            $('.sliderExample').slick('slickNext');
        });
        $(".list-section .list").each(function (e, q) {
            $(q).click(function () {
                var l = $(this).prop("id");
                l = l.substr(3, l.length);
                $('.sliderExample').slick('slickGoTo', l, false);
            });
        });

        $('.sliderExample').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
            $("#app" + currentSlide).find('.c-icon').removeClass('active_icon');
            $("#app" + nextSlide).find('.c-icon').addClass('active_icon');
        });
            $(".screen").hover(function () {
                $(".screen").addClass("animated pulse");
                $('.sliderExample').slick('slickPause');
            }, function () {
                $(".screen").removeClass("animated pulse");
                $('.sliderExample').slick('slickPlay');
            });

    });
    }

    var url = $("#product-url").attr("data-url");
    var name = $("#product-url").attr("data-name");
    loadSlider(url,name);
  
    $(window).resize(function () {
//        location.reload();
    });


//    This is for product features animation overlay effects
    $(".product-features .features").each(function (i) {
        var img = $(this).find('img');
        var img_src = img.attr("data-gif");
        var osrc;
        if (img_src !== undefined) {
            $(this).hover(function () {
                osrc = img.attr("src");
                img.attr("src", img_src);
            }, function () {
                img.attr("src", osrc);
            });
        }
    });


});