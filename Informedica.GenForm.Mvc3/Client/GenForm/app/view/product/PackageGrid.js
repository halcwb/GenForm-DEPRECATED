/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/11/11
 * Time: 11:29 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.PackageGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.packagegrid',

    initComponent: function () {
        var me = this;

        me.store = me.getPackageStore();
        me.columns = this.createColumns();

        this.callParent(arguments)
    },

    createColumns: function () {
        return  [
            { id: 'packagename', header: 'Verpakking', dataIndex: 'PackageName', field: 'textfield'}
        ];
    },

    getPackageStore: function () {
        return Ext.create('GenForm.store.product.PackageName');
    }
});