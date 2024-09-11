(function($) {
    
	"use strict";
	
	$(window).on("load", function() {
		$('.fade-in').css({position:"relative", opacity:0, top:-14});
		
		setTimeout(function() {
			$(".page-loader").fadeOut(400, function() {
				setTimeout(function() {
					$(".fade-in").each(function(index) {
						$(this).delay(400*index).animate({top:0, opacity:1}, 800);
					});
				}, 800);
			});
		}, 400);
	});

	$(document).ready(function() {

		//Create a countdown instance. Change the launchDay according to your needs.
		//The month ranges from 0 to 11. I specify the month from 1 to 12 and manually subtract the 1.
		var launchDay = new Date(2024, 12-1, 25);
		
		$("#timer").countdown({
			until:launchDay
		});

		//Invoke the Placeholder plugin
		$('input, textarea').placeholder();

		//Validate newsletter form
		$('<div class="success"></div>').hide().appendTo('.form-wrap');
		
		$("#newsletter-form").validate({
			rules:{
				email:{
					required:true, 
					email:true
				}
			},
			messages:{
				email:{
					required:"Email address is required",
					email:"Email address is not valid"
				}
			},
			errorElement:"span",
			errorPlacement:function(error, element) {
				error.appendTo(element.parent());
			},
			submitHandler:function(form) {
				$(form).hide();
				$(".page-loader").addClass("overlay").css({opacity:0}).show().animate({opacity:1});
				
				$.post($(form).attr("action"), $(form).serialize(), function(data) {
					$(".page-loader").animate({opacity:0}, function() {
						$(this).hide();
						$("#newsletter .success").show().html(data).animate({opacity:1});
					});
				});
				
				return false;
			}
		});

		//Open modal window on click
		$('#modal-open').on("click", function(e) {
			e.preventDefault();
			
			var mainInner = $("#main .inner"),
				modal = $("#modal");

			mainInner.animate({opacity:0}, 400, function() {
				$("html, body").scrollTop(0);
				modal.addClass("modal-active").fadeIn(400);
			});
			
			$("#modal-close").on("click", function(e) {
				e.preventDefault();
				
				modal.removeClass("modal-active").fadeOut(400, function() {
					mainInner.animate({opacity:1}, 400);
				});
			});
		});
		
		/***************************
			- Image background -
		***************************/
		if($("#bg-image").length>0) {
			$.backstretch("images/image/bg.jpg");
		}
		
		/***********************
			- Image slider -
		***********************/
		if($("#bg-slide").length>0) {
			$.backstretch([
				"images/slide/1.jpg",
				"images/slide/2.jpg",
				"images/slide/3.jpg"
			], {
				fade:750,
				duration:4000
			});
		}
		
		/************************
			- Youtube video -
		************************/
		if($("#bg-video").length>0) {
			$(".player").YTPlayer({
				videoURL:'https://youtu.be/kn-1D5z3-Cs',
				mobileFallbackImage:'images/bg/video.jpg',
				containment:'body',
				autoPlay:true,
				showControls:false,
				mute:true, 
				startAt:0, 
				stopAt:0,
				opacity:1
			});
		}

	});
	
})(jQuery);