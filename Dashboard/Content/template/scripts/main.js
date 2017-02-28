/**
 * Main scripts file
 */
(function ($) {
    'use strict';
    /* Define some variables */
    var app = $('.app'),
        searchState = false,
        menuState = false;

    function toggleMenu() {
        if (menuState) {
            app.removeClass('move-left move-right');
            setTimeout(function () {
                app.removeClass('offscreen');
            }, 150);
        } else {
            app.addClass('offscreen move-right');
        }
        menuState = !menuState;
    }

    /******** Open messages ********/
    $('[data-toggle=message]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        app.toggleClass('message-open');
    });

    /******** Open contact ********/
    $('[data-toggle=contact]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        app.toggleClass('contact-open');
    });

    /******** Toggle expanding menu ********/
    $('[data-toggle=expanding]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        app.toggleClass('expanding');
    });

    /******** Card collapse control ********/
    $('[data-toggle=card-collapse]').on('click', function (e) {
        var parent = $(this).parents('.card'),
            body = parent.children('.card-block');
        if (body.is(':visible')) {
            parent.addClass('card-collapsed');
            body.slideUp(200);
        } else if (!body.is(':visible')) {
            parent.removeClass('card-collapsed');
            body.slideDown(200);
        }
        e.preventDefault();
        e.stopPropagation();
    });

    /******** Card refresh control ********/
    $('[data-toggle=card-refresh]').on('click', function (e) {
        var parent = $(this).parents('.card');
        parent.addClass('card-refreshing');
        window.setTimeout(function () {
            parent.removeClass('card-refreshing');
        }, 3000);
        e.preventDefault();
        e.stopPropagation();
    });

    /******** Card remove control ********/
    $('[data-toggle=card-remove]').on('click', function (e) {
        var parent = $(this).parents('.card');
        parent.addClass('animated zoomOut');
        parent.bind('animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd', function () {
            parent.remove();
        });
        e.preventDefault();
        e.stopPropagation();
    });

    /******** Search form ********/
    

    /******** Sidebar toggle menu ********/
    $('[data-toggle=sidebar]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        toggleMenu();
    });
    $('.main-panel').on('click', function (e) {
        var target = e.target;
        if (menuState && target !== $('[data-toggle=sidebar]') && !$('.header-secondary')) {
            toggleMenu();
        }
    });

    /******** Sidebar menu ********/
    $('.sidebar-panel nav a').on('click', function (e) {
        var $this = $(this),
            links = $this.parents('li'),
            parentLink = $this.closest('li'),
            otherLinks = $('.sidebar-panel nav li').not(links),
            subMenu = $this.next();
        if (!subMenu.hasClass('sub-menu')) {
            toggleMenu();
            return;
        }
        otherLinks.removeClass('open');
        if (subMenu.is('ul') && (subMenu.height() === 0)) {
            parentLink.addClass('open');
        } else if (subMenu.is('ul') && (subMenu.height() !== 0)) {
            parentLink.removeClass('open');
        }
        if (subMenu.is('ul')) {
            return;
        }
        e.stopPropagation();
        return;
    });
    $('.sidebar-panel').find('> li > .sub-menu').each(function () {
        if ($(this).find('ul.sub-menu').length > 0) {
            $(this).addClass('multi-level');
        }
    });

    /******** Demo only ********/
    function getURLParameter(name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
    }

    var layout = getURLParameter('layout');

    if (layout) {
        switch (layout) {
            case "offcanvas":
                $('.app').addClass('offcanvas');
                break;
            case "expanding":
                $('.app').addClass('expanding');
                break;
            case "fixed":
                $('.app').addClass('fixed-header');
                break;
            case "boxed":
                $('.app').addClass('boxed');
                break;
            case "static":
                $('.app').addClass('static');
                break;
        }
    }

    $('.mouseEffects').find('th').find('a').hide();

    $('.mouseEffects').mouseover(function () {
        $(this).find('th').find('a').show();
    });
    $('.mouseEffects').mouseout(function () {
        $(this).find('th').find('a').hide();
    });

    $('.delete-confirmation').on('click', function () {
        var that = $(this);
        swal({
            title: 'Wirklich l\u00f6schen?',
            text: 'Unwideruflich l\u00f6schen!',
            type: 'warning',
            cancelButtonText: 'Nein',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Ja, l\u00f6schen.',
            closeOnConfirm: false
        }, function () {

            var url = that.attr('id');
            $.ajax({
                type: "POST",
                url: url,
                data: {
                    __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
                },
                success: function () {
                    swal({
                        title: 'Erfolgreich',
                        text: 'gel\u00f6scht.',
                        type: 'success',
                    }, function () {
                        window.location.reload();
                    });
                }
            });

        });
    });

    $('.edit-via-ajax').on('click', function () {
        $('form').first().bind('submit', function () {
            var form = $('form').first();
            var data = form.serialize();
            $.ajax({
                type: "POST",
                url: form.attr('action'),
                data: data,
                success: function () {
                    swal({
                        title: 'Erfolgreich',
                        text: 'gespeichert.',
                        type: 'success',
                    }, function () {
                        window.location = form.attr('action').substr(0, form.attr('action').lastIndexOf('/Edit'));
                    });
                }
            });
            return false;
        });	
    });

})(jQuery);
