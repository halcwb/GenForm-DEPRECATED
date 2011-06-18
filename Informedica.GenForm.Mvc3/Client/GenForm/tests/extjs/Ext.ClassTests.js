/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/18/11
 * Time: 10:56 AM
 * To change this template use File | Settings | File Templates.
 */
describe('Ext.Class', function () {
    var instance, getInstance;

    Ext.define('Tests.MyClass', {

         config: {
              name: 'name of the instance'
         },

         constructor: function (config) {
              var me = this;
              me = this.initConfig(config);
              return me;
         },

         // Gets called before setName method
         applyName: function (name) {
              if (!Ext.isString(name)) {
                   alert('Error: name is not a string');
              }
              // need to return name otherwise name does not get set!!
              else {
                   return name;
              }
         }
    });

    getInstance = function (config) {
        if (!instance) {
            return Ext.create('Tests.MyClass', config);
        }
    };

    it('can create an instance of Tests.MyClass', function () {
       expect(getInstance({name: 'test'})).toBeDefined();
    });

    it('an instance of Tests.MyClass should have a getter and setter method for name', function () {
        var instance = getInstance({name: 'test'});
        
        expect(instance.getName).toBeDefined();
        expect(instance.setName).toBeDefined();
    })

});