/**
 * Created by .
 * User: hal
 * Date: 10-5-11
 * Time: 23:12
 * To change this template use File | Settings | File Templates.
 */
describe('Ext.ComponentQuery', function () {
    var createTestForm;

    createTestForm = function () {
        return Ext.create('Ext.form.Panel', {
            id: 'testform',
            title: 'test',
            items: [
                { xtype: 'textfield', name: 'test'}
            ]
        });
    };

    it('a test form should be created', function () {
       expect(createTestForm()).toBeDefined(); 
    });

    it('component query finds the panel', function () {
       expect(Ext.ComponentQuery.query('#testform').length).toBe(1);
    });

    it('should be able to get the testform using the title', function () {
       expect(Ext.ComponentQuery.query('panel[title="test"]').length).toBe(1);
    });

    it('if you go down from testform you get a textfield', function () {
        expect(createTestForm().down('textfield').name).toBe('test');
    });
});