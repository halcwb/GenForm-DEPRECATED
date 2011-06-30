
Ext.define('GenForm.lib.view.component.ToolbarButton', {
    extend: 'Ext.button.Button',
    text: '',
    scale: 'large',
    location: Ext.app.config.appFolder + '/../style/images/',
    iconAlign: 'top',
    disabled: false,
    width: 60,
    
    initComponent:function(){
        var me = this;
        me.icon = me.location + me.icon;

        me.callParent(arguments);
    }
});