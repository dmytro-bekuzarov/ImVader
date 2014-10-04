//Plaeholder handler
$(function() {
	
if(!Modernizr.input.placeholder){             //placeholder for old brousers and IE
 
  $('[placeholder]').focus(function() {
   var input = $(this);
   if (input.val() == input.attr('placeholder')) {
    input.val('');
    input.removeClass('placeholder');
   }
  }).blur(function() {
   var input = $(this);
   if (input.val() == '' || input.val() == input.attr('placeholder')) {
    input.addClass('placeholder');
    input.val(input.attr('placeholder'));
   }
  }).blur();
  $('[placeholder]').parents('form').submit(function() {
   $(this).find('[placeholder]').each(function() {
    var input = $(this);
    if (input.val() == input.attr('placeholder')) {
     input.val('');
    }
   })
  });
 }
  
$('#contact-form').submit(function(e) {
      
		e.preventDefault();	
		var error = 0;
		var self = $(this);
		
	    var $name = self.find('[name=user-name]');
	    $email = self.find('[type=email]');
	    $phone = self.find('[name=user-phone]');
	    $title = self.find('[name=user-title]');
	    $message = self.find('[name=user-message]');
		
				
		var emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
		
  		if(!emailRegex.test($email.val())) {
			createErrTult('Enter proper email', $email)
			error++;	
		}

		if( $name.val().length>1 &&  $name.val()!= $name.attr('placeholder')  ) {
			$name.removeClass('form__field--invalid');			
		} 
		else {
			createErrTult('Enter at least 2 characters', $name)
			error++;
		}

		if( $phone.val().length>1 &&  $phone.val()!= $phone.attr('placeholder')  ) {
			$phone.removeClass('form__field--invalid');			
		} 
		else {
			createErrTult('Enter your phone number', $phone)
			error++;
		}

		if($title.val().length>1 && $title.val()!= $title.attr('placeholder')) {
			$title.removeClass('form__field--invalid');
		} 
		else {
			createErrTult('Enter at least 2 characters', $title)
			error++;
		}

		if($message.val().length>2 && $message.val()!= $message.attr('placeholder')) {
			$message.removeClass('form__field--invalid');
		} 
		else {
			createErrTult('Enter at least 3 characters', $message)
			error++;
		}		
		
		
		if (error!=0)return;
		self.find('[type=submit]').attr('disabled', 'disabled');

		self.children().fadeOut(300,function(){ $(this).remove() })
		$('<p class="success">Thank you!<br>Your message has been successfully sent!</p>').appendTo(self)
		.hide().delay(300).fadeIn();


		var formInput = self.serialize();
		$.post(self.attr('action'),formInput, function(data){}); // end post
}); // end submit

$('#subscribe-form').submit(function(e) {
      
		e.preventDefault();	
		var error = 0;
		var self = $(this);
		
	    var $email = self.find('[type=email]');	
	    	
				
		var emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
		
  		if(!emailRegex.test($email.val())) {
			createErrTult("Enter proper email", $email)
			error++;	
		}	
		
		if (error!=0)return;
		self.find('[type=submit]').attr('disabled', 'disabled');

		self.children().fadeOut(300,function(){ $(this).remove() })
		$('<p>Thank you! We have received your email!</p>').appendTo(self)
		.hide().delay(300).fadeIn();


		var formInput = self.serialize();
		$.post(self.attr('action'),formInput, function(data){}); // end post
}); // end submit
		
		

function createErrTult(text, $elem) {
	$('<p />', {
		'class':'form__tooltip',
		'text':text,
	}).css({'top':$elem.position().top,"left":$elem.position().left})
	.appendTo($elem.addClass('form__field--invalid').parent()) 
	.delay(3000).fadeOut(300, function(){$(this).remove()});
}
			
});

