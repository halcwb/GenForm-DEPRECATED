
Ext.define('GenForm.lib.view.button.ToolbarButton', {
    extend: 'Ext.button.Button',
    alias: 'widget.toolbarbutton',
    text: '',
    scale: 'large',
    location: Ext.app.config.appFolder + '/style/images/',
    iconAlign: 'top',
    disabled: false,
    width: 60,
    
    initComponent:function(){
        var me = this;
        if(me.icon) me.icon = me.location + me.icon;

        me.callParent(arguments);
    }
});