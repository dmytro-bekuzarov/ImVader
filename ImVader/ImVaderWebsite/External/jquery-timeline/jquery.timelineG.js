// JavaScript Document
;(function($){

var defaults ={
		maxdis:320,
		mindis:170,
		wraperClass:'timeline-wrap'
	}
	
	
$.fn.timeLineG=function(options){
	new TimeLine(this.first(),options);
	return this.first();
}
	
	
function TimeLine ($element, options){
	this.config = $.extend({},defaults,options);
	this.container = $element;
	this.offset=0;
	this.init();
}

TimeLine.prototype.init = function(){
	
	var self=this;
	
	self.container.wrap('<div style="overflow:hidden" class="'+self.config.wraperClass+'"/>');
	
	self.container.find('.timeline__year, .timeline__event').each(function(i,e){
		
		self.increaseOffset();
		var $elem = $(e);
		$elem.css('left',self.offset);
		
		var $circle=$elem.find('.circle');
		$circle.data('top',$circle.css('top')).css({'top':0}).removeClass('open-e');	
				
		var $line=$elem.find('.line');
		$line.data('top',$line.css('top')).css({'height':0,'top':0});
				
		$elem.find('.timeline__block').css({'display':'none'});
		
	})
	
		self.increaseOffset();
		self.container.css('width',self.offset);
	
	drag();
	
	function drag(){
		self.container
		.draggable({ axis: 'x', cursor: 'move' })
		.bind('dragstop', function(e, ui) {                            
			var r = ui.position.left + $(this).width();
			if(r < $(window).width())
				self.container.animate({ 'left': ($(window).width()-$(this).width())+'px' });
			if(ui.position.left > 0)
				  self.container.animate({ 'left': '0px' });
		});
	}
	
	$(".timeline__event .circle").on('click',function(){
		var $this =$(this);
		if ($this.hasClass('open-e'))
		{
			$this.delay(200).animate({'top':0}).removeClass('open-e');
			$this.siblings('.line').delay(200).animate({'height':0,'top':0})
			$this.siblings('.timeline__block').fadeOut(200)
		}else{
			$this.animate({'top':$this.data('top')}).addClass('open-e');
			$this.siblings('.line').animate({'height':100,'top':$this.siblings('.line').data('top')})
			$this.siblings('.timeline__block').delay(300).fadeIn()
		}	
	})
	
	var mass = self.container.find('.timeline__event .circle');
	
	for (var i=0, l=mass.length;i<l;){
		
			mass.eq(i).trigger('click');
			
			i+=Math.round(1+Math.random()*2);
	}
}

TimeLine.prototype.increaseOffset = function(){
	this.offset+=this.config.mindis + Math.random()*(this.config.maxdis-this.config.mindis); 
}
	
}(jQuery))