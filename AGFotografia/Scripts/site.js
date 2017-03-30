$(document).ready(function () {

    $("#lightgallery").lightGallery();


    (function ($) {
        "use strict";
        var JSONRequest = {
            1: {
                
            },
            2: {
               
            },
            3: {
               
            }
        }
        var lastLink = '';

        $(function () {
            /*----------  PRELOADER  ----------*/
            setTimeout(function () {
                $('#preloader').animate({ 'opacity': '0' }, 300, function () {
                    $('#preloader').hide();
                    $('#haeder').css('top', $('#home .flexslider').height());
                });
                $('.left-menu, .right-content').animate({ 'opacity': '1' }, 500);
            }, 800);
            /*----------  //PRELOADER  ----------*/

            $('.images-bg').each(function () {
                $(this).css({
                    'background-image': 'url(' + $('img', this).hide().attr('src') + ')',
                    'height': $(window).height()
                });
            });

            /*----------  BIG SLIDER  ----------*/
            $('.portfolio-with-details .flexslider, .service .flexslider').flexslider({ slideshowSpeed: 5000 });
            $('.portfolio-image.flexslider').flexslider({ slideshow: false });
            $('.flexslider.home-page').flexslider({
                slideshowSpeed: 5000,
                after: function (slider) {
                    var next = $('.flex-active-slide', slider).find('.home-title');
                    var className = '';
                    if (next.hasClass('left')) {
                        className = 'bounceInLeft';
                    } else if (next.hasClass('top')) {
                        className = 'flipInX';
                    } else if (next.hasClass('zoom')) {
                        className = 'bounceIn';
                    }
                    next.addClass(className + ' animated');
                    next.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                        next.removeClass(className + ' animated');
                    });
                }
            });
            /*----------  //BIG SLIDER  ----------*/


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

            $('.flex-direction-nav .flex-next').addClass('glyph fa-angle-right').text('');
            $('.flex-direction-nav .flex-prev').addClass('glyph fa-angle-left').text('');
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

                /*----------  ADD SCROLL IN PORTFOLIO  ----------*/
                //baron({
                //    root: '.portfolio',
                //    scroller: '.scroller',
                //    bar: '.scroller__bar',
                //    barOnCls: 'baron'
                //});
                /*----------  //ADD SCROLL IN PORTFOLIO  ----------*/

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
                    /*----------  ADD SCROLL IN BLOG  ----------*/
                    baron({
                        root: '.blog',
                        scroller: '.scroller',
                        bar: '.scroller__bar',
                        barOnCls: 'baron'
                    });
                    /*----------  //ADD SCROLL IN BLOG  ----------*/

                    /*----------  ADD SCROLL IN BLOG SINGLE  ----------*/
                    baron({
                        root: '.blog-single',
                        scroller: '.scroller',
                        bar: '.scroller__bar',
                        barOnCls: 'baron'
                    });
                    /*----------  //ADD SCROLL IN BLOG SINGLE  ----------*/

                    /*----------  ADD SCROLL IN LEFT CONTENT  ----------*/
                    baron({
                        root: '.left-content',
                        scroller: '.scroller',
                        bar: '.scroller__bar',
                        barOnCls: 'baron'
                    });
                    /*----------  //ADD SCROLL LEFT CONTENT  ----------*/

                    /*----------  ADD SCROLL IN RIGHT CONTENT  ----------*/
                    baron({
                        root: '.right-images',
                        scroller: '.scroller',
                        bar: '.scroller__bar',
                        barOnCls: 'baron'
                    });
                    baron({
                        root: '.blog-right',
                        scroller: '.scroller',
                        bar: '.scroller__bar',
                        barOnCls: 'baron'
                    });
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

            setTimeout(function () {
                $('.about div[data-style]').each(function () {
                    $(this).attr('style', $(this).attr('data-style'));
                })
            }, 1000)

            $('.skill .amount').each(function () {
                var value = $(this).html();
                $(this).html('0%');
                var thiz = this;
                setTimeout(function () {
                    $(thiz).countTo({
                        from: 0,
                        to: value,
                        speed: 1000,
                        formatter: function (value, options) {
                            return value.toFixed(options.decimals) + '%';
                        }
                    });
                }, 1000)
            });
            /*----------  //ABOUTE PAGE  ----------*/

            /*----------  FILTER  ----------*/
            $('.filter a').on('click', function () {
                $('.filter a').removeClass('active');
                $(this).addClass('active');
                $('.portfolio li, .masonry-wrapper .item').addClass('unactive');

                if ($(this).text().toLowerCase() == 'all') {
                    $('.portfolio li, .masonry-wrapper .item').removeClass('unactive');
                } else {
                    $('.portfolio li[data-class="' + $(this).text().toLowerCase() + '"], .masonry-wrapper .item[data-class="' + $(this).text().toLowerCase() + '"]').removeClass('unactive');
                }


                return false
            });
            /*----------  //FILTER  ----------*/

             /*----------  DROP DOWN MENU  ----------*/
            $('.show-more').on('click', function () {
                var myTxt = '';

                for (var i in JSONRequest) {
                    myTxt += '<div style="opacity:0;" class="blog-block">'
                        + '<img alt="" src="' + JSONRequest[i].image + '" class="col-lg-6 col-md-6 col-sm-6">'
                        + '<div class="blog-details col-lg-6 col-md-6 col-sm-6">'
                        + '<a href="">' + JSONRequest[i].title + '</a>'
                        + '<p class="mini">' + JSONRequest[i].text + '</p>'
                        + '<div class="blog-info">'
                        + '<a href="">'
                        + '<strong>Admin</strong>'
                        + '</a>'
                        + '<a href="">'
                        + '<strong>Photography</strong>'
                        + '</a>'
                        + '<a href="">'
                        + '<strong>Comments</strong>'
                        + '</a>'
                        + '<span class="glyph fa-comment">' + JSONRequest[i].comment + '</span>'
                        + '</div>'
                        + '</div>'
                        + '<div class="date">'
                        + '<span class="day">25</span>'
                        + '<span class="month">nov</span>'
                        + '</div> '
                    + '</div>';
                }
                $('.left-content-text').append(myTxt);
                $(this).remove();

                setTimeout(function () {
                    $('.blog-block').css('opacity', 1);
                }, 200);
            });
            /*----------  //DROP DOWN MENU  ----------*/

            $('#submit').on('click', function () {
                var flag = true;

                if (/\D/.test($('#phone').val())) {
                    $('#phone').val('').attr('placeholder', 'please enter phone number').addClass('error');
                    flag = false;
                }
                if (!/^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$/.test($('#email').val())) {
                    $('#email').val('').attr('placeholder', 'please enter correct e-mail').addClass('error');;
                    flag = false;
                }
                if (flag) {
                    $(this).parents('form').submit();
                }

                return false
            });
        })
    })(jQuery);

});
