Ext.define('GenForm.lib.view.window.SaveCancelWindow', {
    extend: 'Ext.window.Window',

    requires: [
        'GenForm.lib.view.component.SaveCancelToolbar'
    ],

    constructor: function (config) {
        var me = this;
    
        me = me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.dockedItems = me.createSaveCancelToolbar();

        me.callParent(arguments);
    },

    createSaveCancelToolbar: function () {
        return Ext.create('GenForm.lib.view.component.SaveCancelToolbar', { dock: 'bottom'});
    }

});