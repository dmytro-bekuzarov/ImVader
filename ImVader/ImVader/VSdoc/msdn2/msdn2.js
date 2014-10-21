/***********************************************
 * PAGE INIT
 ***********************************************/ 

/**
 * Splitting the screen
 */ 
function SplitScreen (nonScrollingRegionId, scrollingRegionId) {
	this.nonScrollingRegion = document.getElementById(nonScrollingRegionId);
	this.scrollingRegion = document.getElementById(scrollingRegionId);

	document.body.style.margin = "0px";
	document.body.style.overflow = "hidden";
	this.scrollingRegion.style.overflow = "auto";

	this.resize(null);

	registerEventHandler(window, 'resize', getInstanceDelegate(this, "resize"));
	// clean itself on document unload
	registerEventHandler(window, 'unload', getInstanceDelegate(this, "dispose"));
}

SplitScreen.prototype.resize = function(e) {
	var scrollRegionHeight = document.body.clientHeight - this.nonScrollingRegion.offsetHeight;
	if (scrollRegionHeight < 0) {
		scrollRegionHeight = 0;
	}
	this.scrollingRegion.style.height = scrollRegionHeight;
	this.scrollingRegion.style.width = document.body.clientWidth;
}


SplitScreen.prototype.dispose = function(e) {
	try {
		this.nonScrollingRegion = null;
		this.scrollingRegion = null;
		//remove events so if external document is open in window, it will not be affected
		unregisterEventHandler(window, 'load', init);
		unregisterEventHandler(window, 'resize', getInstanceDelegate(this, "resize"));
		unregisterEventHandler(window, 'unload', getInstanceDelegate(this, "dispose"));
	
		// This is not neccessary.  In IE (6 at least) the "scrolling" attribute from frameset's HTML 
		// is always used when new page is loaded in the frame.
		document.body.style.overflow = "auto";
		document.body.style.height = "100%";
		document.body.scroll="yes";
		window.frameElement.scrolling = "auto";
	} catch (ex) {}
}



// Event handler attachment
function registerEventHandler (element, event, handler) {
	if (element.attachEvent) {
		element.attachEvent('on' + event, handler);
	} else if (element.addEventListener) {
		element.addEventListener(event, handler, false);
	} else {
		element[event] = handler;
	}
}


// Event handler detachment
function unregisterEventHandler(element, event, handler) {
  if (typeof element.removeEventListener == "function")
    element.removeEventListener(event, handler, false);
  else
    element.detachEvent("on" + event, handler);
}


function getInstanceDelegate (obj, methodName) {
	return( function(e) {
		e = e || window.event;
		return obj[methodName](e);
	} );
}


//element manipulation, functions with limited functionality defined not to throw errors on the page
//window.onload=init;
registerEventHandler(window, 'load', init);
		


function init()
{
	try {
		fixImages();
		fixMoniker();
		fixMsdnLinks();
		loadLangFilter();
		showSelectedLanguages();
	
		var langDropDown = document.getElementById("devlangsDropdown");
		var langMenu = document.getElementById("devlangsMenu");
		registerEventHandler(langDropDown,'mouseover', showLangFilter);
		registerEventHandler(langDropDown,'mouseout', hideLangFilter);
		registerEventHandler(langMenu,'mouseover', showLangFilter);
		registerEventHandler(langMenu,'mouseout', hideLangFilter);
	}
	catch (e) {}
	try {
		expandAll(document.getElementById("toggleAllImage"));
	} catch (e2) {}
		// split screen
	try {
		// hide frame scrollbar in HTML output (not in CHM or Help2)
		if (window.name=="vbdocright") {
			//it's HTML output in frame
			document.body.scroll="no";
			window.frameElement.scrolling = "no";
		}  
	
		var screen = new SplitScreen('header', 'mainSection');
	}	
	catch (e3) {}
}

/***********************************************
 * END PAGE INIT
 ***********************************************/ 



/***********************************************
 * LANGUAGE FILTER
 ***********************************************/ 
    
function loadLangFilter() {
	var inputTags = document.getElementsByTagName("input");
	var i;
	for (i=0; i<inputTags.length; i++) {
		switch (inputTags[i].getAttribute("id")) {
			case "VisualBasicCheckbox":
				inputTags[i].checked = Boolean(loadSetting("showVB", true));
				break;
			case "CSharpCheckbox":
				inputTags[i].checked = Boolean(loadSetting("showCsharp", true));
				break;
			case "ManagedCPlusPlusCheckbox":
				inputTags[i].checked = Boolean(loadSetting("showCpp", true));
				break;
			case "JScriptCheckbox":
				inputTags[i].checked = Boolean(loadSetting("showJscript", true));
				break;
			case "JSharpCheckbox":
				inputTags[i].checked = Boolean(loadSetting("showJsharp", true));
				break;
		}
	}
}


function saveLangFilter() {
	var inputTags = document.getElementsByTagName("input");
	var i;
	for (i=0; i<inputTags.length; i++) {
		switch (inputTags[i].getAttribute("id")) {
			case "VisualBasicCheckbox":
				saveSetting("showVB", inputTags[i].checked);
				break;
			case "CSharpCheckbox":
				saveSetting("showCsharp", inputTags[i].checked);
				break;
			case "ManagedCPlusPlusCheckbox":
				saveSetting("showCpp", inputTags[i].checked);
				break;
			case "JScriptCheckbox":
				saveSetting("showJscript", inputTags[i].checked);
				break;
			case "JSharpCheckbox":
				saveSetting("showJsharp", inputTags[i].checked);
				break;
		}
	}
}


function showLangFilter() {
	var langDropDown = document.getElementById("devlangsDropdown");
	var langMenu = document.getElementById("devlangsMenu");
	
	var pos = getElementPosition(langDropDown);
	langMenu.style.left = pos[0];
	langMenu.style.top = pos[1] + langDropDown.offsetHeight - 1;
	langMenu.style.visibility = "visible";
}

function hideLangFilter() {
	var langMenu = document.getElementById("devlangsMenu");
	langMenu.style.visibility = "hidden";
}


function getElementPosition (element) {
	var eleft = 0;
	var etop = 0;
	
	do {
		eleft += element.offsetLeft;
		etop += element.offsetTop;
		element = element.offsetParent;
	} while (element);
	
	return [eleft, etop];
}



/**
 * Hides/shows the language sections according to language filter
 */ 
function SetLanguageEx(key) {
	showSelectedLanguages();
	saveLangFilter();

	// call also original method from MSDN if available
	try {
		SetLanguage(key);
	} catch (ex) {
	}
}


/**
 * Hides/shows the language sections according to language filter
 */ 
function showSelectedLanguages() {
	try {
		var showVB = true;
		var showCsharp = true;
		var showCpp = true;
		var showJscript = true;
		var showJsharp = true;
		var showXaml = true;
		var selectedCount = 0;
		var allCount = 0;
		
		var inputTags = document.getElementsByTagName("input");
		var i;
		for (i=0; i<inputTags.length; i++) {
			switch (inputTags[i].getAttribute("id")) {
				case "VisualBasicCheckbox":
					allCount++;
					if (!inputTags[i].checked) {
						showVB = false;
					} else {
						selectedCount++;
					}
					showHideTag(document.getElementById("devlangsMenuVisualBasicLabel"), showVB);
					break;
				case "CSharpCheckbox":
					allCount++;
					if (!inputTags[i].checked) {
						showCsharp = false;
					} else {
						selectedCount++;
					}
					showHideTag(document.getElementById("devlangsMenuCSharpLabel"), showCsharp);
					break;
				case "ManagedCPlusPlusCheckbox":
					allCount++;
					if (!inputTags[i].checked) {
						showCpp = false;
					} else {
						selectedCount++;
					}
					showHideTag(document.getElementById("devlangsMenuManagedCPlusPlusLabel"), showCpp);
					break;
				case "JScriptCheckbox":
					allCount++;
					if (!inputTags[i].checked) {
						showJscript = false;
					} else {
						selectedCount++;
					}
					showHideTag(document.getElementById("devlangsMenuJScriptLabel"), showJscript);
					break;
				case "JSharpCheckbox":
					allCount++;
					if (!inputTags[i].checked) {
						showJsharp = false;
					} else {
						selectedCount++;
					}
					showHideTag(document.getElementById("devlangsMenuJSharpLabel"), showJsharp);
					break;
			}
		}
		
		// update filter text
		showHideTag(document.getElementById("devlangsMenuAllLabel"), false);
		showHideTag(document.getElementById("devlangsMenuMultipleLabel"), false);
		if (selectedCount > 1) {
			showHideTag(document.getElementById("devlangsMenuVisualBasicLabel"), false);
			showHideTag(document.getElementById("devlangsMenuCSharpLabel"), false);
			showHideTag(document.getElementById("devlangsMenuManagedCPlusPlusLabel"), false);
			showHideTag(document.getElementById("devlangsMenuJSharpLabel"), false);
			showHideTag(document.getElementById("devlangsMenuJScriptLabel"), false);

			if (selectedCount == allCount) {
				showHideTag(document.getElementById("devlangsMenuAllLabel"), true);
			} else { 
				showHideTag(document.getElementById("devlangsMenuMultipleLabel"), true);
			}
		}
		
		var sTags = document.getElementsByTagName("span");
		for (i=0; i<sTags.length; i++) {
			switch (sTags[i].getAttribute("codeLanguage")) {
				case "VisualBasicDeclaration":
				case "VisualBasic":
					showHideTag(sTags[i], showVB);
					break;
				case "CSharp":
					showHideTag(sTags[i], showCsharp);
					break;
				case "ManagedCPlusPlus":
					showHideTag(sTags[i], showCpp);
					break;
				case "JScript":
					showHideTag(sTags[i], showJscript);
					break;
				case "JSharp":
					showHideTag(sTags[i], showJsharp);
					break;
			}
		}
	} catch (ex) {}
}


function showHideTag(tag, visible) {
	try {
		if (visible) {
			tag.style.display = "";
		} else {
			tag.style.display = "none";
		}
	} catch (e) {
	}
}



/***********************************************
 * END LANGUAGE FILTER
 ***********************************************/ 




/***********************************************
 * COPY CODE
 ***********************************************/ 

function ChangeCopyCodeIcon(spanItem) {
	try {
		var imageItem = getCopyCodeImageItem(spanItem);
		if (imageItem.src == document.getElementById("copyImage").src) {
			imageItem.src = document.getElementById("copyHoverImage").src;
		} else {
			imageItem.src = document.getElementById("copyImage").src;
		}
	} catch (ex) {}
}


function getCopyCodeImageItem(spanItem) {
	var kids = spanItem.childNodes;
	for (var i = 0; i < kids.length; i++) {
		if (kids[i].className == "copyCodeImage") {
			return kids[i];
		}
	}
	return null;
}


function CopyCode(item) {
	try {
		// find table element
		var table = item.parentNode;
		while (table && (table.nodeName.toLowerCase() != "table")) {
			table = table.parentNode;
		}
		// get last row
		var kids = table.childNodes;

		for (var i = 0; i < kids.length; i++) {
			if (kids[i].nodeName.toLowerCase() == "tbody") {
				// go into tbody if any
				kids = kids[i].childNodes;
				break;
			}
		}
		
		var tr = null;
		for (i = 0; i < kids.length; i++) {
			if (kids[i].nodeName.toLowerCase() == "tr") {
				tr = kids[i];
			}
		}

		// get td and text
		if (tr != null) {
			var kids = tr.childNodes;
			var td = null
			
			for (i = 0; i < kids.length; i++) {
				if (kids[i].nodeName.toLowerCase() == "td") {
					td = kids[i];
					break;
				}
			}
			
			// get code and remove <br>
			var code;
			code = td.innerHTML;
			code = code.replace(/<br>/gi, "\n");
			code = code.replace(/<\/td>/gi, "</td>\n");	// syntax highlighter removes \n chars and puts each line in separate <td>
			// get plain text
			var tmpDiv = document.createElement('div');
			tmpDiv.innerHTML = code;

			if (typeof(tmpDiv.textContent) != "undefined") {
				// standards compliant
				code = tmpDiv.textContent;
			}
			else if (typeof(tmpDiv.innerText) != "undefined") {
				// IE only
				code = tmpDiv.innerText;
			}

			try {
				// works in IE only
				window.clipboardData.setData("Text", code);
			} catch (ex) {
				popCodeWindow(code);
			}
		}
	} catch (e) {}
}


function CopyCode_CheckKey(spanItem)
{
	if(window.event.keyCode == 13)
		CopyCode(spanItem);
}


function popCodeWindow(code) {
	try {
		var codeWindow =  window.open ("",
			"Copy the selected code",
			"location=0,status=0,toolbar=0,menubar =0,directories=0,resizable=1,scrollbars=1,height=400, width=400");
		codeWindow.document.writeln("<html>");
		codeWindow.document.writeln("<head>");
		codeWindow.document.writeln("<title>Copy the selected code</title>");
		codeWindow.document.writeln("</head>");
		codeWindow.document.writeln("<body bgcolor=\"#FFFFFF\">");
		codeWindow.document.writeln('<pre id="code_text">');
		codeWindow.document.writeln(escapeHTML(code));
		codeWindow.document.writeln("</pre>");
		codeWindow.document.writeln("<scr" + "ipt>");
		// the selectNode function below, converted by http://www.howtocreate.co.uk/tutorials/jsexamples/syntax/prepareInline.html 
		var ftn = "function selectNode (node) {\n\tvar selection, range, doc, win;\n\tif ((doc = node.ownerDocument) && \n\t\t(win = doc.defaultView) && \n\t\ttypeof win.getSelection != \'undefined\' && \n\t\ttypeof doc.createRange != \'undefined\' && \n\t\t(selection = window.getSelection()) && \n\t\ttypeof selection.removeAllRanges != \'undefined\') {\n\t\t\t\n\t\trange = doc.createRange();\n\t\trange.selectNode(node);\n    selection.removeAllRanges();\n    selection.addRange(range);\n\t} else if (document.body && \n\t\t\ttypeof document.body.createTextRange != \'undefined\' && \n\t\t\t(range = document.body.createTextRange())) {\n     \n\t\t \trange.moveToElementText(node);\n     \trange.select();\n  }\n} ";
		codeWindow.document.writeln(ftn);
		codeWindow.document.writeln("selectNode(document.getElementById('code_text'));</scr" + "ipt>");
		codeWindow.document.writeln("</body>");
		codeWindow.document.writeln("</html>");
		codeWindow.document.close();
	} catch (ex) {}
}


function escapeHTML (str) {                                       
	return str.replace(/&/g,"&amp;").                                         
		replace(/>/g,"&gt;").
		replace(/</g,"&lt;").
		replace(/"/g,"&quot;");                                         
}

function selectNode (node) {
	var selection, range, doc, win;
	if ((doc = node.ownerDocument) && 
		(win = doc.defaultView) && 
		typeof win.getSelection != 'undefined' && 
		typeof doc.createRange != 'undefined' && 
		(selection = window.getSelection()) && 
		typeof selection.removeAllRanges != 'undefined') {
			
		range = doc.createRange();
		range.selectNode(node);
    selection.removeAllRanges();
    selection.addRange(range);
	} else if (document.body && 
			typeof document.body.createTextRange != 'undefined' && 
			(range = document.body.createTextRange())) {
     
		 	range.moveToElementText(node);
     	range.select();
  }
} 

/***********************************************
 * END COPY CODE
 ***********************************************/ 


/***********************************************
 * PERSISTENCE
 ***********************************************/ 

/**
 * Sets the cookie value.
 * name - name of the cookie
 * value - value of the cookie
 * [expires] - expiration date of the cookie (defaults to end of current session)
 * [path] - path for which the cookie is valid (defaults to path of calling document)
 * [domain] - domain for which the cookie is valid (defaults to domain of calling document)
 * [secure] - Boolean value indicating if the cookie transmission requires a secure transmission
 * an argument defaults when it is assigned null as a placeholder
 * a null placeholder is not required for trailing omitted arguments
 */
function setCookie(name, value, expires, path, domain, secure) {
  var curCookie = name + "=" + escape(value) +
      ((expires) ? "; expires=" + expires.toGMTString() : "") +
      ((path) ? "; path=" + path : "") +
      ((domain) ? "; domain=" + domain : "") +
      ((secure) ? "; secure" : "");
  document.cookie = curCookie;
}

/**
 * Gets the cookie value.
 * name - name of the desired cookie
 * return string containing value of specified cookie or null if cookie does not exist
 */ 
function getCookie(name) {
  var dc = document.cookie;
  var prefix = name + "=";
  var begin = dc.indexOf("; " + prefix);
  if (begin == -1) {
    begin = dc.indexOf(prefix);
    if (begin != 0) return null;
  } else
    begin += 2;
  var end = document.cookie.indexOf(";", begin);
  if (end == -1)
    end = dc.length;
  return unescape(dc.substring(begin + prefix.length, end));
}


// name - name of the cookie
// [path] - path of the cookie (must be same as path used to create cookie)
// [domain] - domain of the cookie (must be same as domain used to create cookie)
// * path and domain default if assigned null or omitted if no explicit argument proceeds
function deleteCookie(name, path, domain) {
  if (getCookie(name)) {
    document.cookie = name + "=" +
    ((path) ? "; path=" + path : "") +
    ((domain) ? "; domain=" + domain : "") +
    "; expires=Thu, 01-Jan-70 00:00:01 GMT";
  }
}


// fixMoniker is needed to implement userData in a CHM
//
function fixMoniker() {
  var curURL = document.location + ".";
  var pos = curURL.indexOf("mk:@MSITStore");
  if( pos == 0 ) {
    curURL = "ms-its:" + curURL.substring(14,curURL.length-1);
    document.location.replace(curURL);
    return false;
  }
  else { return true; }
}


/**
 * Detects whether HTML5 localStotage functionality is supported.
 */ 
function isLocalStorageSupported() {
    var str = 'vsdocmanDetectStorage';
    try {
        localStorage.setItem(str, str);
        localStorage.removeItem(str);
        return true;
    } catch(e) {
        return false;
    }
}


function saveSetting(name, value) {
	// create an instance of the Date object
	var now = new Date();
	// cookie expires in one year (actually, 365 days)
	// 1000 milliseconds in a second
	now.setTime(now.getTime() + 365 * 24 * 60 * 60 * 1000);
	// convert the value to correct String
	if (value.constructor==Boolean) {
		if (!value) {
			value = "";
		}
	}
	// IE returns wrong document.cookie if the value is empty string
	if (value == "") {
		value = "string:empty"
	}
	
	// try to use localStorage (instead of old-fashioned cookies or userData)
	if(isLocalStorageSupported()) {
		localStorage.setItem(name, value);
		return;
  }
	
	// we cannot use cookies in CHM, so try to use behaviors if possible
	var headerDiv;	// we can use any particular DIV or other element
	headerDiv = document.getElementById("header");
	if (headerDiv.addBehavior) {
		headerDiv.style.behavior = "url('#default#userData')";
		headerDiv.expires = now.toUTCString();
		headerDiv.setAttribute(name, value);
		// Save the persistence data as "helpSettings".
		headerDiv.save("helpSettings");
	} else {
		// set the new cookie
		setCookie(name, value, now/*, "/"*/);
	}
}


function loadSetting(name, defaultValue) {
	var res;

	// try to use localStorage (instead of old-fashioned cookies or userData)
	if(isLocalStorageSupported()) {
		res = localStorage.getItem(name);
  } else {
		// we cannot use cookies in CHM, so try to use behaviors if possible
		var headerDiv;	// we can use any particular DIV or other element
		headerDiv = document.getElementById("header");
		if (headerDiv.addBehavior) {
			headerDiv.style.behavior = "url('#default#userData')";
			headerDiv.load("helpSettings");
			res = headerDiv.getAttribute(name);
		} else {
			// get the cookie
			res = getCookie(name);
		}
	}
	 
	if (res == "string:empty") {
		res = "";
	} 
	if (res == null) {
		res = defaultValue;
	}
	return res;
}


/***********************************************
 * END PERSISTENCE
 ***********************************************/ 



/***********************************************
 * STUFF DEFINED IN MSDN AND NEEDED BY PAGES
 ***********************************************/ 

function OpenSection(imageItem)
{
}


function loadAll(){
}

function saveAll(){
}

function OnLoadImage(event) {
}

/***********************************************
 * END STUFF DEFINED IN MSDN AND NEEDED BY PAGES
 ***********************************************/ 



/***********************************************
 * EXPAND/COLLAPSE
 ***********************************************/ 

/**
 * Fixes the src attribute of img tags because some contain
 * escaped and some unescaped URIs. This causes problems if
 * external script compares them (e.g. in MSDN v9.0).  
 */ 
function fixImages() {
	var images = document.getElementsByTagName("img");
	var i;
	for (i = 0; i < images.length; i++) {
		images[i].src = unescape(images[i].src);
	}
}


function ExpandCollapse(imageItem) {
	if (unescape(imageItem.src) == unescape(document.getElementById("expandImage").src)) {
		expand(imageItem);
	}
	else {
		collapse(imageItem);
	}
}

// helper
function expand(imageItem) {
	imageItem.src = document.getElementById("collapseImage").src;
	imageItem.alt = document.getElementById("collapseImage").alt;
	var section = imageItem.parentNode.parentNode;
	do {
		section = section.nextSibling;
  } while (section && !section.tagName || section.tagName.toLowerCase() != "div");
  if (section != null) {
		section.style.display	= "";
  } 		
}

// helper
function collapse(imageItem) {
	imageItem.setAttribute("src", document.getElementById("expandImage").getAttribute("src"));
	imageItem.alt = document.getElementById("expandImage").alt;
	var section = imageItem.parentNode.parentNode;
	do {
		section = section.nextSibling;
  } while (section && !section.tagName || section.tagName.toLowerCase() != "div");
  if (section != null) {
		section.style.display	= "none";
  } 		
}


function ExpandCollapse_CheckKey(imageItem) {
	if(window.event.keyCode == 13)
		ExpandCollapse(imageItem);
}

function OpenSection(imageItem)
{
}

function ExpandCollapseAll(imageItem) {
	if (unescape(imageItem.src) == unescape(document.getElementById("expandAllImage").src)) {
		expandAll(imageItem)
	}
	else {
		collapseAll(imageItem);
	}
}

//helper
function expandAll(imageItem) {
	var switches = document.getElementsByName("toggleSwitch");
	
	imageItem.src = document.getElementById("collapseAllImage").src;
	imageItem.alt = document.getElementById("collapseAllImage").alt;
	document.getElementById("collapseAllLabel").style.display = "inline";
	document.getElementById("expandAllLabel").style.display = "none";

	for (var i = 0; i < switches.length; ++i) {
		expand(switches[i]);
	}
}

//helper
function collapseAll(imageItem) {
	var switches = document.getElementsByName("toggleSwitch");
	
	imageItem.src = document.getElementById("expandAllImage").src;
	imageItem.alt = document.getElementById("expandAllImage").alt;
	document.getElementById("collapseAllLabel").style.display = "none";
	document.getElementById("expandAllLabel").style.display = "inline";

	for (var i = 0; i < switches.length; ++i) {
		collapse(switches[i]);
	}
}

/***********************************************
 * EXPAND/COLLAPSE
 ***********************************************/ 


/***********************************************
 * MSDN LINKS FIX
 ***********************************************/ 

/**
 * Fixes all links that should point to MSDN. This
 * applies to CHM documentation. 
 */ 
function fixMsdnLinks()
{
	msdnVersion = getHighestMsdnVersion ();
	//get all MSDN links
	var msdnLinks = new Array();
	var allLinks = document.getElementsByTagName("a");
	var i;
	for (i=0; i<allLinks.length; i++) {
		if (allLinks[i].getAttribute("href").toLowerCase().substr(0,8) == "ms-help:" ) {
			msdnLinks[msdnLinks.length] = allLinks[i]; 
			fixMsdnLink(allLinks[i]);
		}
	}
}


/**
 * Fixes one link that should point to MSDN.
 */ 
function fixMsdnLink(linkElement) {
	var href = linkElement.getAttribute("href");
	if (href.toLowerCase().substr(0,8) == "ms-help:") {
		var keyword = href.replace(/(.*keyword=\")([^\"]+)(\".*)/g, "$2");
		//fix original MSDN link
		var link;
		switch (msdnVersion) {
			case "9.0":
				link = 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="';
				link = link + escapeKeyword(keyword);
				link = link + '";?index="!DefaultAssociativeIndex"';
				break;
			case "8.0":
				link = 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="';
				link = link + escapeKeyword(keyword);
				link = link + '";?index="!DefaultAssociativeIndex"';
				break;
			default:
				//web
				link = convertMsdnKeywordToMsdn2Link(keyword);
				break
		}
		linkElement.setAttribute("href", link);
	}
}


/**
 * Fixes MSDN keyword for use with redirect.htm in CHM format.
 */ 
function escapeKeyword(keyword) {
  /*
  Keyword can contain "," characters as parameter delimiter. 
  Keyword resolving engine expects TAB where keyword syntax uses ",".
  It also expects NEWLINE where keyword syntax uses ";". While HxLink.htc
  used with <MSHelp:link> in HxS format takes care of it, redirect.htm 
  used in CHM format doesn't. So we must do it.
   */ 
  var res = keyword; 
  res = res.replace(/,/g, "\t");
  res = res.replace(/;/g, "\n");
  res = escape(res);
  return res;
}


/**
 * Converts MSDN2 keyword (cref) to MSDN2 web link
 */ 
function convertMsdnKeywordToMsdn2Link(keyword) {
	var res = keyword;
	
	// remove prefix
	res = res.replace(/^.+:(.*)/g, "$1");
	// remove parameters
	res = res.replace(/(.*)\(.*/g, "$1");
	
	res = res.toLowerCase();
	res = "http://msdn.microsoft.com/en-us/library/" + res + ".aspx";
	return res;
}


// Highest MSDN version installed.
var msdnVersion;


/**
 * Returns highest MSDN version installed.
 * @return The MSDN version found in format 8.0 or 9.0 or web. If none is found,
 *         the "web" is returned.  
 */ 
function getHighestMsdnVersion () {
  var MSDN_9_CSS = "ms-help://MS.VSCC.v90/dv_vscccommon/styles/presentation.css";
	var MSDN_8_CSS = "ms-help://MS.VSCC.v80/dv_vscccommon/local/Classic.css"; 

	if (cssFileExists(MSDN_9_CSS)) {
		return "9.0";
	}
	if (cssFileExists(MSDN_8_CSS)) {
		return "8.0";
	}

  return "web";
}


/**
 * Tests whether specified CSS url exists.
 */ 
function cssFileExists(cssUrl) {
	var sheet = null
	var temporary = false
	res = false;

	// first detect whether this CSS is already used in this document (it should be)
  try {
    var i;
    for (i=0; i<document.styleSheets.length; i++) {
      if (document.styleSheets[i].href.toLowerCase() == cssUrl.toLowerCase()) {
        sheet = document.styleSheets[i];
        break;
      }    
    }
  } catch (ex) {
  }
  
	// now check, if the sheet really contains any rules - the CSS file exists
	try {
		if (sheet.rules) {
			// IE
			if (sheet.rules.length > 0) {
				res = true
			}
		} else if (sheet.cssRules) {
			// FF
			if (sheet.cssRules.length > 0) {
				res = true
			}
		}
	} catch (ex) {
	}

	return res;
}

/***********************************************
 * END MSDN LINKS FIX
 ***********************************************/ 
