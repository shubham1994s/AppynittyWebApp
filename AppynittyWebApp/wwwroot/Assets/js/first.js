$(function(){
    if ($(document).width() < 768) {
        $('.sliderExample').slick({
            dots: false,
            arrows: false,
            autoplay: false,
            asNavFor: '.home-flex',
        });
    
        $('.home-flex').slick({
            dots: false,
            arrows: false,
            autoplay: true,
            asNavFor: '.sliderExample',
        });
    }else{
        $('.sliderExample').slick({
            dots: false,
            arrows: false,
            autoplay: false,           
        });
    }
    
});


var f=$(window).width();
var w=$(window).width()/2;
var fw="first";
$(".home-flex").mousemove(function (event) {
    
    event.preventDefault();
//    console.log("pageX: " + event.pageX + ", pageY: " + event.pageY);
//    console.log(event.pageX);
//    console.log(event.screenX);
      x=event.pageX;  
//    console.log(w/2);
    if(event.screenX < w){
        
        $(".home-first-section .right").clearQueue();
        $(".home-first-section .right").animate({"width":(f-x)+"px"},0,"linear");
        if (fw=="first" || fw=="left") {
            fw="right";
    //            console.log("Right: " + (f - x));
                $(".home-first-section .left").css("zIndex", "-1");
                $(".home-first-section .right").css("zIndex", "1");
    //        $(".home-first-section .mobile").removeClass("left-img");
    //        $(".home-first-section .mobile").addClass("right-img");

                $(".home-first-section .home-flex").removeClass("right-white");
                $(".home-first-section .home-flex").addClass("left-white");
            $('.sliderExample').slick('slickNext');
//                $('.sliderExample').slick('slickPrev');
        }        
    }   
    
    if(event.screenX > w){
        
        $(".home-first-section .left").clearQueue();
        $(".home-first-section .left").animate({"width":x+"px"},0,"linear");
        
        if (fw == "first" || fw == "right") {
            fw = "left";
                $(".home-first-section .right").css("zIndex","-1");
                $(".home-first-section .left").css("zIndex","1");
       //         $(".home-first-section .mobile").removeClass("right-img");
       //         $(".home-first-section .mobile").addClass("left-img");

                $(".home-first-section .home-flex").removeClass("left-white");
               $(".home-first-section .home-flex").addClass("right-white");
               $('.sliderExample').slick('slickPrev'); 
//             $('.sliderExample').slick('slickGoTo', 2,false);
        }        
    }
});

$(".home-first-section .home-flex").mouseleave(function(){
//    fw = "start";
    c=w;//-150;
    $(".home-first-section .left").css({
                    "width":c+"px",
                    "zIndex":"0"
                });
    $(".home-first-section .right").css("width",c+"px");
    $(".home-first-section .home-flex").removeClass("right-white");
    $(".home-first-section .home-flex").removeClass("left-white");
    
});
