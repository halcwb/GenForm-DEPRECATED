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
        debugger;
        me.callParent(config);
    },

    initComponent: function () {
        var me = this;

        //me.dockedItems = me.createSaveCancelToolBar();

        me.callParent(arguments);
    },

    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.lib.view.component.SaveCancelToolbar', { dock: 'bottom'});
    }

});