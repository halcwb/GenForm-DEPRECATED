Ext.define('GenForm.lib.view.window.SaveCancelWindow', {
    extend: 'Ext.window.Window',

    mixins: ['GenForm.lib.util.mixin.FormCreator'],
    closeAction: 'destroy',

    constructor: function (config) {
        var me = this;
        
        me = me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;
        
        me.toolbar = {};
        me.dockedItems = me.createSaveCancelToolbar();

        me.callParent(arguments);
    },

    createSaveCancelToolbar: function () {
        var me = this;
        me.toolbar = Ext.create('GenForm.lib.view.component.SaveCancelToolbar', { dock: 'bottom'});
        return me.toolbar;
    }

});