/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:58 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.PackageWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.packagewindow',

    width: 500,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.items = this.createPackageForm();

        this.callParent(arguments);
    },

    createPackageForm: function () {
        return { xtype: 'packageform' };
    },

    getPackageForm: function () {
        return this.items.items[0];
    },

    loadWithPackage: function (package) {
        this.getPackageForm().getForm().loadRecord(package);
    }

});