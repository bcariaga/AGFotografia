$(document).ready(function () {

 

    (function ($) {
        "use strict";

        var lastLink = '';

        $(function () {

            /*----------  CLICK ON HOME PAGE BUTTON  ----------*/
            $('.flexslider .button-down, #portfolioLink').on('click', function (e) {
                if (!($(e.target).parents('#portfolioLink') && e.target.nodeName == 'A')
                    && !$('.home-page').hasClass('animate')
                    || $(e.target).hasClass('button-down')) {

                    $('.home-page').addClass('animate');
                    $('.main-navi a').removeClass('active');

                    setTimeout(function () {
                        $('.portfolio').removeClass('animate');
                    });
                    if ($(".player").length) {
                        $('.mb_YTVPPlaypause').trigger('click');
                    }

                    if (lastLink.length) {
                        lastLink.addClass('active');
                    } else {
                        $('#portfolioLink').addClass('active');
                    }

                    return false
                }
            });
            /*----------  //CLICK ON HOME PAGE BUTTON  ----------*/
           

            /*----------  SHOW HIDE MAIN MENU  ----------*/
            $('.main-navi').on('click', '#showHideMenu', function () {
                if ($('.left-menu').hasClass('animate')) {
                    $('.left-menu').removeClass('animate');
                    if ($(window).width() <= 480) {
                        $('.left-menu').css('left', -180);
                    } else if ($(window).width() <= 640) {
                        $('.left-menu').css('left', -200);
                    } else {
                        $('.left-menu').css('left', -280);
                    }

                    $('.main-navi').css('margin-right', 15);
                } else {
                    $('.left-menu').addClass('animate');
                    $('.left-menu').css('left', 0);
                    $('.main-navi').css('margin-right', 0);
                }

                return false
            });
            /*----------  //SHOW HIDE MAIN MENU  ----------*/

 
            /*----------  SHOW PORTFOLIO DETAILS  ----------*/
            setTimeout(function () {
                $('.portfolio.column span, .portfolio.horizontal li, .masonry-wrapper span').on('click', function () {
                    if ($(this).parents('li').hasClass('unactive')) {
                        return false
                    }
                    $('.portfolio').addClass('animate');
                });
            }, 1000);

            $('.right-images').on('click', 'span', function () {
                lastLink = $('.main-navi a.active, .main-navi > div.active');
                $('.main-block').addClass('animate');
                $('.main-navi a, .main-navi > div').removeClass('active');
                $('#portfolioLink').addClass('active');
            });

            //$('.flex-direction-nav .flex-next').addClass('glyph fa-angle-right').text('');
            //$('.flex-direction-nav .flex-prev').addClass('glyph fa-angle-left').text('');
            /*----------  //SHOW PORTFOLIO DETAILS  ----------*/

            /*----------  HIDE PORTFOLIO DETAILS  ----------*/
            $('.button-close, .details-close').on('click', function () {
                if (lastLink.length) {
                    $('.main-navi a, .main-navi > div').removeClass('active');
                    lastLink.addClass('active');
                }
                $('.main-block').removeClass('animate');

                return false
            });
            /*----------  //HIDE PORTFOLIO DETAILS  ----------*/
            /*----------  CLICK ON HOME LINK  ----------*/
            $('#homeLink').on('click', function () {
                $('.home-page').removeClass('animate');
                lastLink = $('.main-navi a.active, .main-navi > div.active');
                $('.main-navi a, .main-navi > div').removeClass('active');
                $(this).addClass('active');

                if ($(".player").length) {
                    $('.mb_YTVPPlaypause').trigger('click');
                }
                return false;
            });
            /*----------  //CLICK ON HOME LINK  ----------*/
            setTimeout(function () {


                /*----------  PORTFOLIO WITH 3 COLUMN  ----------*/
                $('#threeColumn .scroller').gridrotator({
                    columns: 3,
                    rows: 999,
                    animType: 'fadeInOut',
                    animSpeed: 1000,
                    interval: 2000,
                    step: 1,
                    w1024: { rows: 999, columns: 2 },
                    w768: { rows: 999, columns: 2 },
                    w480: { rows: 999, columns: 2 },
                    w320: { rows: 999, columns: 1 },
                })
                /*----------  //PORTFOLIO WITH 3 COLUMN  ----------*/


                /*----------  MASONRY PORTFOLIO  ----------*/
                var container = document.querySelector('#masonry');
                if (container) {
                    var msnry = new Masonry(container, {
                        itemSelector: '.item'
                    });
                }
                /*----------  //MASONRY PORTFOLIO  ----------*/
            }, 500);


  
            if ($(window).width() > 1024) {
                setTimeout(function () {
                  

                    /*----------  ADD SCROLL IN RIGHT CONTENT  ----------*/
                    baron({
                        root: '.right-images',
                        scroller: '.scroller',
                        bar: '.scroller__bar',
                        barOnCls: 'baron'
                    });
                    //baron({
                    //    root: '.blog-right',
                    //    scroller: '.scroller',
                    //    bar: '.scroller__bar',
                    //    barOnCls: 'baron'
                    //});
                    /*----------  //ADD SCROLL RIGHT CONTENT  ----------*/

                    /*----------  RIGHT IMAGE PORTFOLIO  ----------*/
                    $('.right-images li').each(function () {
                        $(this).css({
                            'height': Math.floor($('.right-images').width() * 100 / 133.33),
                            'width': $('.right-images').width()
                        });
                    });
                    /*----------  //RIGHT IMAGE PORTFOLIO  ----------*/
                }, 500)


            } else if ($(window).width() > 320) {

                /*----------  RIGHT IMAGE PORTFOLIO  ----------*/
                setTimeout(function () {
                    $('.right-images li').each(function () {
                        $(this).css({
                            'height': Math.floor($('.right-images').width() / 2 * 100 / 133.33),
                            'width': $('.right-images').width() / 2
                        });
                    });
                }, 500)
                /*----------  //RIGHT IMAGE PORTFOLIO  ----------*/

            } else {
                /*----------  RIGHT IMAGE PORTFOLIO  ----------*/
                $('.right-images li').each(function () {
                    $(this).css({
                        'height': Math.floor($('.right-images').width() * 100 / 133.33),
                        'width': $('.right-images').width()
                    });
                });
                /*----------  //RIGHT IMAGE PORTFOLIO  ----------*/
            }

            /*----------  ABOUTE PAGE  ----------*/

           
        })
    })(jQuery);

});
