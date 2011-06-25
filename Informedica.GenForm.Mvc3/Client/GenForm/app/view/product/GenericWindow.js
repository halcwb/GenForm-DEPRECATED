/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:54 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.GenericWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.genericwindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.items = this.createGenericForm();

        this.callParent(arguments);
    },

    createGenericForm: function () {
        return { xtype: 'genericform' };
    },

    getGenericForm: function () {
        return this.items.items[0];
    },

    loadWithGeneric: function (generic) {
        this.getGenericForm().getForm().loadRecord(generic);
    }

});