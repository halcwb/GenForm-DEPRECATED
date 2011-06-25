/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 11:02 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.UnitWindow', {
    extend: 'Ext.Window',
    alias: 'widget.unitwindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.items = this.createUnitForm();

        this.callParent(arguments);
    },

    createUnitForm: function () {
        return { xtype: 'unitform' };
    },

    getUnitForm: function () {
        return this.items.items[0];
    },

    loadWithUnit: function (unit) {
        this.getUnitForm().getForm().loadRecord(unit);
    }

});