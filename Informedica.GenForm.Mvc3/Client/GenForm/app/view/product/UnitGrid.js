/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/11/11
 * Time: 11:18 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.UnitGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.unitgrid',

    // TODO: temp hack because of the me.loadMask.bindStore problem
    viewConfig: {
        loadMask: false
    },

    initComponent: function () {
        var me = this;

        me.store = me.getUnitStore();
        me.columns = this.createColumns();

        this.callParent(arguments)
    },

    createColumns: function () {
        return  [
            { id: 'unitname', header: 'Eenheid', dataIndex: 'UnitName', field: 'textfield'}
        ];
    },

    getUnitStore: function () {
        return Ext.create('GenForm.store.product.UnitName');
    }
});