Ext.define('GenForm.view.component.EditableComboBox',{
    extend:'Ext.form.field.ComboBox',
    alias:['widget.editablecombo', 'widget.editcombo'],
    trigger1Cls:Ext.baseCSSPrefix+'form-clear-trigger',
    trigger2Cls:Ext.baseCSSPrefix+'form-arrow-trigger',
    trigger3Cls:Ext.baseCSSPrefix + 'form-add-trigger',
/*
    trigger4Cls:Ext.baseCSSPrefix + 'form-edit-trigger',
*/

    onTrigger2Click:function(){
        var me=this;
        if(!me.readOnly&&!me.disabled){
            if(me.isExpanded){me.collapse();}
            else{
                me.onFocus({});
                if(me.triggerAction==='all'){me.doQuery(me.allQuery,true);}
                else{me.doQuery(me.getRawValue());}
            }
            me.inputEl.focus();
        }
    },

    onTrigger1Click:function(){
    	var me=this;
    	me.clearValue();
    },

    onTrigger3Click: function () {
        Ext.MessageBox.alert('Edit or Add item');
    }
});