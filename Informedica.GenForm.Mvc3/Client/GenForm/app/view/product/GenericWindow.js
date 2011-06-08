/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:54 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.GenericWindow', {
    extend: 'Ext.Window',
    alias: 'widget.genericwindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.dockedItems = this.createSaveCancelToolBar();
        this.items = this.createGenericForm();

        this.callParent(arguments);
    },

    createGenericForm: function () {
        return { xtype: 'genericform' };
    },

    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.view.component.SaveCancelToolBar', { dock: 'bottom'});
    },

    getGenericForm: function () {
        return this.items.items[0];
    },

    loadWithGeneric: function (generic) {
        this.getGenericForm().getForm().loadRecord(generic);
    }

});