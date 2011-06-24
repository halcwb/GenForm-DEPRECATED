/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/19/11
 * Time: 10:06 AM
 * To change this template use File | Settings | File Templates.
 */
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