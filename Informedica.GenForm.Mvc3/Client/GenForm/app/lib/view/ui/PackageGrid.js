Ext.define('GenForm.lib.view.ui.PackageGrid', {
    extend: 'Ext.grid.Panel',

    itemId: 'grdPackage',
    title: 'Verpakkingen',

    initComponent: function () {
        var me = this;

        me.store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetPackageNames }).load();
        me.columns =  [
            {
                xtype: 'gridcolumn',
                hidden: true,
                itemId: 'colId',
                dataIndex: 'Id',
                text: 'String'
            },
            {
                xtype: 'gridcolumn',
                itemId: 'colName',
                dataIndex: 'Name',
                text: 'Naam'
            }
        ];

        me.callParent(arguments);
    }
});