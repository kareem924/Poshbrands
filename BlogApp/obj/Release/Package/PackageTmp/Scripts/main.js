(function ($) {
    "use strict";

    $(document).ready(function () {

        /*---------------------
         1. slicknav
        --------------------- */
        $('#mobile-menu').slicknav({
            allowParentLinks: true
        });
        $(".toggleTitle").click(function () { $(this).parent().find($(".toggleContent")).slideToggle(); $(this).toggleClass("active"); $(".toggleContent").not($(this).parent().find($(".toggleContent"))).slideUp(); $(".toggleTitle").not($(this)).removeClass("active") });

        $(".careersSubmenuResponsive").click(function () { $(".careersSubmenu").slideToggle() });
        $(".bag-icon").on('click', function () {
            $(".shopping-cart").slideToggle();
            $(".top-account").css("display", "none");
            $(".top-search").css("display", "none");
        });
        $(".lock-icon").on('click', function () {
            $(".top-account").slideToggle();
            $(".shopping-cart").css("display", "none");
            $(".top-search").css("display", "none");
        });
        $(".search-icon").on('click', function () {
            $(".top-search").slideToggle();
            $(".shopping-cart").css("display", "none");
            $(".top-account").css("display", "none");
        });

        $(".nav-open").on('click', function () {
            $(".main-menu-area").fadeToggle(2000);
        });
        $(".nav-close").on('click', function () {
            $(".main-menu-area").css("display", "none");
        });

        /*---------------------
         3. tooltip
        --------------------- */
        $('[data-toggle="tooltip"]').tooltip();

        /*---------------------
         3. TOP Menu Stick
        --------------------- */
        var s = $("#sticker");
        var pos = s.position();
        $(window).scroll(function () {
            var windowpos = $(window).scrollTop();
            if (windowpos > pos.top) {
                s.addClass("stick");
            } else {
                s.removeClass("stick");
            }
        });

        /*---------------------
          scrollUp
        --------------------- */
        $.scrollUp({
            scrollText: '<i class="fa fa-angle-up"></i>',
            easingType: 'linear',
            scrollSpeed: 900,
            animation: 'fade'
        });

        /*---------------------
        Projects filter with shuffle.js
        --------------------- */
        $('.portfolio-content').shuffle({ itemSelector: '.project' });

        $('.portfolio-menu .btn').on('click', function () {

            var filterBtn = $(this),
                isActive = filterBtn.hasClass('active'),
                group = isActive ? 'all' : filterBtn.data('group');

            // Hide current label, show current label in title
            if (!isActive) {
                $('.portfolio-menu .active').removeClass('active');
            }

            filterBtn.toggleClass('active');

            // Filter elements
            $('.portfolio-content').shuffle('shuffle', group);
        });
        /*---------------------
        Image Zoom
        --------------------- */
        $('.simpleLens-big-image').simpleLens({
            loading_image: 'demo/images/loading.gif'
        });

        /*---------------------
        fancybox
        --------------------- */
        $('.fancybox').fancybox();

        /*---------------------
         1. owl-carousel
        --------------------- */
        $(".testimonials-list").owlCarousel({

            navigation: false, // Show next and prev buttons
            slideSpeed: 300,
            paginationSpeed: 400,
            singleItem: true

            // "singleItem:true" is a shortcut for:
            // items : 1, 
            // itemsDesktop : false,
            // itemsDesktopSmall : false,
            // itemsTablet: false,
            // itemsMobile : false

        });
        /*---------------------
        2. owl-carousel
       --------------------- */
        $(".product-carousal").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 4,
            itemsDesktop: [1199, 4],
            itemsDesktopSmall: [979, 3],
            itemsMobile: [767, 1],

        });
        /*---------------------
        3. owl-carousel
       --------------------- */
        $(".blog-carosul").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: false,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: true,
            items: 2,
            itemsDesktop: [1199, 2],
            itemsDesktopSmall: [979, 2],
            itemsMobile: [767, 1],

        });
        /*---------------------
        4. owl-carousel
       --------------------- */
        $(".band-logo-area").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 7,
            itemsDesktop: [1199, 7],
            itemsDesktopSmall: [979, 5],
            itemsMobile: [767, 1]

        });
        /*---------------------
        5. owl-carousel
        --------------------- */
        $(".product-carousal-home-2").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 2],
            itemsMobile: [767, 1]

        });
        /*---------------------
        6. owl-carousel
        --------------------- */
        $(".blog-carosul-home2").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 4,
            itemsDesktop: [1199, 4],
            itemsDesktopSmall: [979, 3],
            itemsMobile: [767, 1]

        });
        /*---------------------
        7. owl-carousel
        --------------------- */
        $(".blog-carosul-home-3").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 2,
            itemsDesktop: [1199, 2],
            itemsDesktopSmall: [979, 2],
            itemsMobile: [767, 1]

        });
        /*---------------------
        8. owl-carousel
        --------------------- */
        $(".product-carousal-5").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 8,
            itemsDesktop: [1199, 8],
            itemsDesktopSmall: [979, 8]

        });
        /*---------------------
        9. owl-carousel
        --------------------- */
        $(".blog-carosul-6").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 4,
            itemsDesktop: [1199, 4],
            itemsDesktopSmall: [979, 4]

        });

        /*---------------------
        10. owl-carousel
        --------------------- */
        $(".post-slider").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: true,
            items: 1,
            itemsDesktop: [1199, 1],
            itemsDesktopSmall: [979, 1],
            itemsMobile: [767, 1]

        });
        /*---------------------
        11. owl-carousel
        --------------------- */
        $(".logo-brand-carosol").owlCarousel({

            autoPlay: false, //Set AutoPlay to 3 seconds
            loop: false,
            navigation: true,
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            pagination: false,
            items: 5,
            itemsDesktop: [1199, 5],
            itemsDesktopSmall: [979, 5]

        });

        /* --------------------------------------------------------
         payment-accordion
      * -------------------------------------------------------*/
        $(".payment-accordion").collapse({
            accordion: true,
            open: function () {
                this.slideDown(550);
            },
            close: function () {
                this.slideUp(550);
            }
        });
        /* --------------------------------------------------------
         FAQ-accordion
      * -------------------------------------------------------*/
        $('.panel-heading a').on('click', function () {
            $('.panel-default').removeClass('actives');
            $(this).parents('.panel-default').addClass('actives');
        });
        /*-------------------------
          showlogin toggle function
        --------------------------*/
        $('#showlogin').on('click', function () {
            $('#checkout-login').slideToggle(900);
        });

        /*-------------------------
          showcoupon toggle function
        --------------------------*/
        $('#showcoupon').on('click', function () {
            $('#checkout_coupon').slideToggle(900);
        });

        /*-------------------------
          Create an account toggle function
        --------------------------*/
        $('#cbox').on('click', function () {
            $('#cbox_info').slideToggle(900);
        });

        /*-------------------------
          Create an account toggle function
        --------------------------*/
        $('#ship-box').on('click', function () {
            $('#ship-box-info').slideToggle(1000);
        });

        /*---------------------
         about-counter
        --------------------- */
        $('.about-counter').counterUp({
            delay: 50,
            time: 3000
        });

        /*---------------------
        PRICE FILTER
        --------------------- */
        $(function () {
            $("#slider-range").slider({
                range: true,
                min: 0,
                max: 100,
                values: [0, 45],
                slide: function (event, ui) {
                    $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
                }
            });
            $("#amount").val("$" + $("#slider-range").slider("values", 0) +
              " - $" + $("#slider-range").slider("values", 1));
        });
        /*---------------------
         countdown
        --------------------- */
        $('[data-countdown]').each(function () {
            var $this = $(this), finalDate = $(this).data('countdown');
            $this.countdown(finalDate, function (event) {
                $this.html(event.strftime('<span class="cdown days"><span class="time-count">%-D</span> <p>Days</p></span> <span class="cdown hour"><span class="time-count">%-H</span> <p>Hour</p></span> <span class="cdown minutes"><span class="time-count">%M</span> <p>Min</p></span> <span class="cdown second"> <span><span class="time-count">%S</span> <p>Sec</p></span>'));
            });
        });

    });

})(jQuery);