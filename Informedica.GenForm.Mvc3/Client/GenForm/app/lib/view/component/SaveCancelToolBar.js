Ext.define('GenForm.lib.view.component.SaveCancelToolbar', {
    extend: 'Ext.toolbar.Toolbar',
    alias: 'widget.savecanceltoolbar',

    mixins: ['GenForm.lib.util.mixin.ButtonCreator'],

    constructor: function (config) {
        var me = this;
        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;
        me.buttons = {};
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createItems: function () {
        var me = this;
        return [
            me.createButton({ text: 'Opslaan', action: 'save'}),
            me.createButton({ text: 'Cancel',  action: 'cancel'})
        ];
    }
});