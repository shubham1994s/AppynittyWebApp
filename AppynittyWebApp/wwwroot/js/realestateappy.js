$.noConflict();
jQuery(document).ready(function ($) {
    if ($(document).width() > 1200)
    {
        $('.fancybox').fancybox({
            padding: 0,
            maxWidth: $(document).height(),
            maxHeight: $(document).width(),
            fitToView: true,
            width: '70%',
            height: '82.2%',
            autoSize: false,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none',
            overlayShow: false,
        });
    }
    else
    {
        $('.fancybox').fancybox({
            padding: 0,
            maxWidth: $(document).height(),
            maxHeight: $(document).width(),
            fitToView: false,
            width: '100%',
            height: '100%',
            autoSize: false,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none',
            overlayShow: false,
        });
    }
    
    
//    $(".right-arrow").click(function(){        
//        $("#app-img").addClass("slideInUp");
//        setTimeout(function(){
//            $("#app-img").removeClass("slideInUp");            
//        },100);
//    });
//    $(".left-arrow").click(function(){        
//        $("#app-img").addClass("slideInLeft");
//        setTimeout(function(){
//            $("#app-img").removeClass("slideInLeft");            
//        },100);
//    });
    
});

