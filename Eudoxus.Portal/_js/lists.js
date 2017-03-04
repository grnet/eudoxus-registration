function addItem(list1,txt1) {
    var list = $get(list1);
    var txt = $get(txt1);
    var url = txt.value;
	var option = document.createElement('option');
	option.value=url;
	option.text=url;
	list.add(option, null); // doesn't work in IE
//  try {
//     }
//    catch(ex) {
//    list.add(option);// IE only
//    }
	txt.value='';
}

function addItem1(list1,txt) {
    var list = $get(list1);
    var url = txt;
	var option = document.createElement('option');
	option.value=url;
	option.text=url;
	 list.add(option, null); // doesn't work in IE
//  try {
//     }
//    catch(ex) {
//    list.add(option);// IE only
//    }
}

function clearList(list1) {
    var list = $get(list1);
	for (i = list.length - 1; i>=0; i--)
		  list.remove(i);
}

function delSelectedItem(list1) {
    var list = $get(list1);
	for (i = list.length - 1; i>=0; i--)
		if (list.options[i].selected)
		  list.remove(i);
}

function getValuesAsCommaSeperated(list) {
//    list = $get(list1);
	var value = '';
	for (i = 0; i < list.length; i++)
		value += list.options[i].value + ',';
	if(value.endsWith(','))
		value = value.substr(0, value.length - 1);
	return value;
}
//function hideEditor(modalId) 
//{
//        var mpeEdit = $find(modalId);
//        if (mpeEdit) {
//            mpeEdit.hide();
//        }       
//}

//function okEditor(modalId,list,text) {
//    var str = getValuesAsCommaSeperated(list);
//	//var txt = $get(text);
//	txt.value = str;
//	var mpeEdit = $find(modalId);
//    if (mpeEdit) 
//        mpeEdit.hide();
//}

//function showModalPopupViaClient(modalId,text,url) 
//{
//    var mpeEdit = $find(modalId);
//    if (mpeEdit) 
//    {
//        mpeEdit.show();
//        //var tmp = $get(text).value.split(',');
//        var tmp = text.value.split(',');
//		clearList(url);
//		for(var i=0; i<tmp.length; i++)
//		{
//		    if(tmp[i].trim() != '')
//		    {
//			    addItem1(url, tmp[i]);
//			}
//		}
//    }
//}