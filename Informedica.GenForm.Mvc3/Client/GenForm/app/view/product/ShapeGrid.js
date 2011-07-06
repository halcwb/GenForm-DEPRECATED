/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/11/11
 * Time: 11:00 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ShapeGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.shapegrid',

    // TODO: temp hack because of the me.loadMask.bindStore problem
    viewConfig: {
        loadMask: false
    },

    initComponent: function () {
        var me = this;

        me.store = me.getShapeStore();
        me.columns = this.createColumns();

        this.callParent(arguments)
    },

    createColumns: function () {
        return  [
            { id: 'shapename', header: 'Vorm', dataIndex: 'ShapeName', field: 'textfield'}
        ];
    },

    getShapeStore: function () {
        return Ext.create('GenForm.store.product.ShapeName');
    }
});